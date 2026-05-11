using Hook.Application.Contracts.Marketplace.Orders;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/marketplace/orders")]
    public class MarketplaceOrdersController(IMarketplaceOrderService orderService) : ControllerBase
    {
        [HttpPost("admin-user/create")]
        [Authorize(Policy = Permissions.MarketplaceOrders_Create)]
        public async Task<IActionResult> Create(CreateMarketplaceOrderRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.CreateAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin-user/my-purchases")]
        [Authorize(Policy = Permissions.MarketplaceOrders_View)]
        public async Task<IActionResult> MyPurchases(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.GetMyPurchasesAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin-seller/my-orders")]
        [Authorize(Policy = Permissions.MarketplaceOrders_View)]
        public async Task<IActionResult> MySellerOrders(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.GetMySellerOrdersAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPatch("admin-seller/out-for-delivery/{orderId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceOrders_UpdateStatus)]
        public async Task<IActionResult> MarkOutForDelivery(Guid orderId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.MarkOutForDeliveryAsync(orderId, userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPatch("admin-seller/cancel/{orderId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceOrders_UpdateStatus)]
        public async Task<IActionResult> SellerCancel(Guid orderId, [FromQuery] string reason, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.SellerCancelAsync(orderId, userId, reason, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPatch("admin-user/cancel/{orderId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceOrders_Cancel)]
        public async Task<IActionResult> BuyerCancel(Guid orderId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.BuyerCancelAsync(orderId, userId, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPatch("admin-user/confirm-received/{orderId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceOrders_UpdateStatus)]
        public async Task<IActionResult> ConfirmReceived(Guid orderId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await orderService.BuyerConfirmReceivedAsync(orderId, userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }


}
