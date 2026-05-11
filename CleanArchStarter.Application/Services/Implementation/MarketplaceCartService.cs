using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Cart;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceCartService(
     IMarketplaceCartRepository cartRepository,
     IMarketplaceProductRepository productRepository,
     IUnitOfWork unitOfWork) : IMarketplaceCartService
    {
        public async Task<Result<MarketplaceCartResponse>> GetMyCartAsync(string buyerUserId, CancellationToken cancellationToken = default)
        {
            var items = await cartRepository.GetByBuyerUserIdAsync(buyerUserId);
            return Result.Success(ToCartResponse(items));
        }

        public async Task<Result<MarketplaceCartResponse>> AddToCartAsync(string buyerUserId, AddToCartRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Quantity <= 0)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.InvalidQuantity);

            var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId);
            if (product is null)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.ProductNotFound);
            if (!product.IsActive)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.ProductInactive);

            if (product.StockQuantity < request.Quantity)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.InsufficientStock);

            var existing = await cartRepository.GetByBuyerAndProductAsync(buyerUserId, request.ProductId);
            if (existing is null)
            {
                var item = new MarketplaceCartItem
                {
                    BuyerUserId = buyerUserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                await cartRepository.AddAsync(item);
            }
            else
            {
                var newQty = existing.Quantity + request.Quantity;
                if (product.StockQuantity < newQty)
                    return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.InsufficientStock);

                existing.Quantity = newQty;
                cartRepository.Update(existing);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var items = await cartRepository.GetByBuyerUserIdAsync(buyerUserId);
            return Result.Success(ToCartResponse(items));
        }

        public async Task<Result<MarketplaceCartResponse>> UpdateQuantityAsync(string buyerUserId, UpdateCartItemQuantityRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Quantity <= 0)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.InvalidQuantity);

            var product = await productRepository.GetByIdWithDetailsAsync(request.ProductId);
            if (product is null)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.ProductNotFound);
            if (!product.IsActive)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.ProductInactive);

            if (product.StockQuantity < request.Quantity)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.InsufficientStock);

            var existing = await cartRepository.GetByBuyerAndProductAsync(buyerUserId, request.ProductId);
            if (existing is null)
                return Result.Failure<MarketplaceCartResponse>(MarketplaceCartErrors.ProductNotFound);

            existing.Quantity = request.Quantity;
            cartRepository.Update(existing);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var items = await cartRepository.GetByBuyerUserIdAsync(buyerUserId);
            return Result.Success(ToCartResponse(items));
        }

        public async Task<Result> RemoveAsync(string buyerUserId, Guid productId, CancellationToken cancellationToken = default)
        {
            var existing = await cartRepository.GetByBuyerAndProductAsync(buyerUserId, productId);
            if (existing is null)
                return Result.Success();

            cartRepository.Delete(existing);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> ClearAsync(string buyerUserId, CancellationToken cancellationToken = default)
        {
            await cartRepository.ClearAsync(buyerUserId);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private static MarketplaceCartResponse ToCartResponse(System.Collections.Generic.IEnumerable<MarketplaceCartItem> items)
        {
            var list = items.Select(i =>
            {
                var p = i.Product;
                var main = p.Images.FirstOrDefault(x => x.IsMainImage)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl;
                var sellerName = p.SellerProfile?.User != null ? $"{p.SellerProfile.User.FirstName} {p.SellerProfile.User.LastName}".Trim() : "Unknown";

                return new MarketplaceCartItemResponse(
                    p.Id,
                    p.Title,
                    p.Price,
                    i.Quantity,
                    p.StockQuantity,
                    p.Condition,
                    p.Category,
                    main,
                    p.SellerProfileId,
                    sellerName
                );
            }).ToList();

            var subtotal = list.Sum(x => x.Price * x.Quantity);
            return new MarketplaceCartResponse(list, subtotal);
        }
    }


}
