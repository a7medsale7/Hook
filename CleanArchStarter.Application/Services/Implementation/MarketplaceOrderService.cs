using Hangfire;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Orders;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceOrderService(
     IMarketplaceOrderRepository orderRepository,
     IMarketplaceProductRepository productRepository,
     IMarketplaceCartRepository cartRepository,
     ISellerProfileRepository sellerProfileRepository,
     IEmailSender emailSender,
     IBackgroundJobClient backgroundJobClient,
     IUnitOfWork unitOfWork) : IMarketplaceOrderService
    {
        private readonly IMarketplaceOrderRepository _orderRepository = orderRepository;
        private readonly IMarketplaceProductRepository _productRepository = productRepository;
        private readonly IMarketplaceCartRepository _cartRepository = cartRepository;
        private readonly ISellerProfileRepository _sellerProfileRepository = sellerProfileRepository;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IBackgroundJobClient _backgroundJobClient = backgroundJobClient;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IEnumerable<MarketplaceOrderResponse>>> CreateAsync(string buyerUserId, CreateMarketplaceOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Items is null || request.Items.Count == 0)
                return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.NoItems);

            foreach (var item in request.Items)
                if (item.Quantity <= 0) return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.InvalidQuantity);

            // Load products and validate availability/stock
            var products = new List<MarketplaceProduct>();
            foreach (var item in request.Items)
            {
                var p = await _productRepository.GetByIdWithDetailsAsync(item.ProductId);
                if (p is null) return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.ProductNotFound);
                if (!p.IsActive) return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.ProductInactive);
                if (p.StockQuantity < item.Quantity) return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.InsufficientStock);
                products.Add(p);
            }

            // Group by seller (cart may contain items from multiple sellers => multiple orders)
            var groups = request.Items
                .Join(products, i => i.ProductId, p => p.Id, (i, p) => new { i, p })
                .GroupBy(x => x.p.SellerProfileId)
                .ToList();

            var createdOrders = new List<MarketplaceOrder>();

            foreach (var g in groups)
            {
                var sellerProfileId = g.Key;
                var sellerProfile = await _sellerProfileRepository.GetByIdAsync(sellerProfileId);
                if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                    return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.SellerNotApproved);

                var order = new MarketplaceOrder
                {
                    BuyerUserId = buyerUserId,
                    SellerProfileId = sellerProfileId,
                    Status = MarketplaceOrderStatus.Pending,
                    PaymentMethod = request.PaymentMethod,
                    ContactEmail = request.ContactEmail,
                    ContactPhone = request.ContactPhone,
                    Governorate = request.Governorate,
                    City = request.City,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    PostalCode = request.PostalCode
                };

                foreach (var x in g)
                {
                    var lineTotal = x.p.Price * x.i.Quantity;
                    order.Items.Add(new MarketplaceOrderItem
                    {
                        OrderId = order.Id,
                        ProductId = x.p.Id,
                        Quantity = x.i.Quantity,
                        UnitPrice = x.p.Price,
                        LineTotal = lineTotal
                    });

                    // Stock decrease immediately (your step 1 rule)
                    x.p.StockQuantity -= x.i.Quantity;
                    _productRepository.Update(x.p);
                }

                order.SubTotal = order.Items.Sum(i => i.LineTotal);
                order.Total = order.SubTotal; // no shipping company, seller arranges delivery => no shipping fee

                await _orderRepository.AddAsync(order);
                createdOrders.Add(order);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.ClearCartItems)
            {
                // remove only purchased items
                foreach (var item in request.Items)
                {
                    var existing = await _cartRepository.GetByBuyerAndProductAsync(buyerUserId, item.ProductId);
                    if (existing != null) _cartRepository.Delete(existing);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            // Email buyer + seller for each order
            foreach (var order in createdOrders)
            {
                var detailed = await _orderRepository.GetByIdWithDetailsAsync(order.Id);
                if (detailed == null) continue;

                try
                {
                    // Buyer email
                    if (!string.IsNullOrWhiteSpace(detailed.ContactEmail))
                    {
                        var html = EmailTemplates.GetMarketplaceOrderCreatedBuyerTemplate(detailed);
                        _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(detailed.ContactEmail, "✅ Order created successfully", html));
                    }

                    // Seller email notification (delivery is seller responsibility)
                    var sellerEmail = detailed.SellerProfile?.User?.Email;
                    if (!string.IsNullOrWhiteSpace(sellerEmail))
                    {
                        var html = EmailTemplates.GetMarketplaceNewOrderSellerTemplate(detailed);
                        _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(sellerEmail, "📦 New marketplace order", html));
                    }
                }
                catch { }
            }

            var responses = createdOrders
                .Select(o => o.Id)
                .Select(async id => (await _orderRepository.GetByIdWithDetailsAsync(id))!)
                .Select(t => t.Result)
                .Where(x => x != null)
                .Select(ToResponse)
                .ToList();

            return Result.Success<IEnumerable<MarketplaceOrderResponse>>(responses);
        }

        public async Task<Result<IEnumerable<MarketplaceOrderResponse>>> GetMyPurchasesAsync(string buyerUserId, CancellationToken cancellationToken = default)
        {
            var orders = await _orderRepository.GetByBuyerUserIdAsync(buyerUserId);
            return Result.Success(orders.Select(ToResponse));
        }

        public async Task<Result<IEnumerable<MarketplaceOrderResponse>>> GetMySellerOrdersAsync(string sellerUserId, CancellationToken cancellationToken = default)
        {
            var seller = await _sellerProfileRepository.GetByUserIdAsync(sellerUserId);
            if (seller is null)
                return Result.Failure<IEnumerable<MarketplaceOrderResponse>>(MarketplaceOrderErrors.Forbidden);

            var orders = await _orderRepository.GetBySellerProfileIdAsync(seller.Id);
            return Result.Success(orders.Select(ToResponse));
        }

        public async Task<Result<MarketplaceOrderResponse>> MarkOutForDeliveryAsync(Guid orderId, string sellerUserId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(orderId);
            if (order is null) return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.OrderNotFound);

            var seller = await _sellerProfileRepository.GetByUserIdAsync(sellerUserId);
            if (seller is null || order.SellerProfileId != seller.Id)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.Forbidden);

            if (order.Status != MarketplaceOrderStatus.Pending)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.InvalidStatus);

            order.Status = MarketplaceOrderStatus.OutForDelivery;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // notify buyer
            try
            {
                var html = EmailTemplates.GetMarketplaceOrderOutForDeliveryBuyerTemplate(order);
                _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(order.ContactEmail, "🚚 Your order is out for delivery", html));
            }
            catch { }

            return Result.Success(ToResponse(order));
        }

        public async Task<Result<MarketplaceOrderResponse>> SellerCancelAsync(Guid orderId, string sellerUserId, string reason, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.CancellationReasonRequired);

            var order = await _orderRepository.GetByIdWithDetailsAsync(orderId);
            if (order is null) return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.OrderNotFound);

            var seller = await _sellerProfileRepository.GetByUserIdAsync(sellerUserId);
            if (seller is null || order.SellerProfileId != seller.Id)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.Forbidden);

            if (order.Status != MarketplaceOrderStatus.Pending)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.InvalidStatus);

            order.Status = MarketplaceOrderStatus.Cancelled;
            order.CancellationReason = reason.Trim();

            // restore stock
            foreach (var item in order.Items)
            {
                var p = await _productRepository.GetByIdWithDetailsAsync(item.ProductId);
                if (p != null)
                {
                    p.StockQuantity += item.Quantity;
                    _productRepository.Update(p);
                }
            }

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            try
            {
                var html = EmailTemplates.GetMarketplaceOrderCancelledBuyerTemplate(order, order.CancellationReason);
                _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(order.ContactEmail, "⚠️ Order cancelled by seller", html));
            }
            catch { }

            return Result.Success(ToResponse(order));
        }

        public async Task<Result> BuyerCancelAsync(Guid orderId, string buyerUserId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(orderId);
            if (order is null) return Result.Failure(MarketplaceOrderErrors.OrderNotFound);

            if (order.BuyerUserId != buyerUserId)
                return Result.Failure(MarketplaceOrderErrors.Forbidden);

            if (order.Status != MarketplaceOrderStatus.Pending)
                return Result.Failure(MarketplaceOrderErrors.InvalidStatus);

            order.Status = MarketplaceOrderStatus.Cancelled;
            order.CancellationReason = "Cancelled by buyer";

            foreach (var item in order.Items)
            {
                var p = await _productRepository.GetByIdWithDetailsAsync(item.ProductId);
                if (p != null)
                {
                    p.StockQuantity += item.Quantity;
                    _productRepository.Update(p);
                }
            }

            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<MarketplaceOrderResponse>> BuyerConfirmReceivedAsync(Guid orderId, string buyerUserId, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.GetByIdWithDetailsAsync(orderId);
            if (order is null) return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.OrderNotFound);

            if (order.BuyerUserId != buyerUserId)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.Forbidden);

            if (order.Status != MarketplaceOrderStatus.OutForDelivery)
                return Result.Failure<MarketplaceOrderResponse>(MarketplaceOrderErrors.InvalidStatus);

            order.Status = MarketplaceOrderStatus.DeliveredConfirmedByBuyer;
            _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            try
            {
                var buyerHtml = EmailTemplates.GetMarketplaceOrderDeliveredBuyerTemplate(order);
                _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(order.ContactEmail, "✅ Order delivered", buyerHtml));

                var sellerEmail = order.SellerProfile?.User?.Email;
                if (!string.IsNullOrWhiteSpace(sellerEmail))
                {
                    var sellerHtml = EmailTemplates.GetMarketplaceOrderDeliveredSellerTemplate(order);
                    _backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(sellerEmail, "✅ Order delivered successfully", sellerHtml));
                }
            }
            catch { }

            return Result.Success(ToResponse(order));
        }

        private static MarketplaceOrderResponse ToResponse(MarketplaceOrder o)
        {
            var sellerName = o.SellerProfile?.User != null ? $"{o.SellerProfile.User.FirstName} {o.SellerProfile.User.LastName}".Trim() : "Unknown";
            var items = o.Items.Select(i =>
            {
                var p = i.Product;
                var main = p?.Images?.FirstOrDefault(x => x.IsMainImage)?.ImageUrl ?? p?.Images?.FirstOrDefault()?.ImageUrl;
                return new MarketplaceOrderItemResponse(
                    i.ProductId,
                    p?.Title ?? "Unknown",
                    i.UnitPrice,
                    i.Quantity,
                    i.LineTotal,
                    p?.Category ?? MarketplaceProductCategory.FishingRods,
                    p?.Condition ?? MarketplaceProductCondition.New,
                    main
                );
            }).ToList();

            return new MarketplaceOrderResponse(
                o.Id,
                o.Status,
                o.PaymentMethod,
                o.SubTotal,
                o.Total,
                o.CreatedOn,
                sellerName,
                o.SellerProfileId,
                items,
                o.ContactEmail,
                o.ContactPhone,
                o.Governorate,
                o.City,
                o.FirstName,
                o.LastName,
                o.Address,
                o.PostalCode,
                o.CancellationReason
            );
        }
    }


}
