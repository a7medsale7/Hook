using Hook.Application.Contracts.Marketplace.Cart;
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
    [Route("api/marketplace/cart")]
    public class MarketplaceCartController(IMarketplaceCartService cartService) : ControllerBase
    {
        [HttpGet("admin-user/my")]
        [Authorize(Policy = Permissions.MarketplaceCart_View)]
        public async Task<IActionResult> GetMyCart(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await cartService.GetMyCartAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("admin-user/add")]
        [Authorize(Policy = Permissions.MarketplaceCart_Update)]
        public async Task<IActionResult> Add(AddToCartRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await cartService.AddToCartAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("admin-user/update-quantity")]
        [Authorize(Policy = Permissions.MarketplaceCart_Update)]
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemQuantityRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await cartService.UpdateQuantityAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("admin-user/remove/{productId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceCart_Update)]
        public async Task<IActionResult> Remove(Guid productId, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await cartService.RemoveAsync(userId, productId, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("admin-user/clear")]
        [Authorize(Policy = Permissions.MarketplaceCart_Update)]
        public async Task<IActionResult> Clear(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await cartService.ClearAsync(userId, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }

}
