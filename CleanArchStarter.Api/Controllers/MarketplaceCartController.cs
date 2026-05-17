using Hook.Application.Contracts.Marketplace.Cart;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/cart")]
    public class MarketplaceCartController(IMarketplaceCartService cartService) : ControllerBase
    {
        private string GetUserIdOrGuestId()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            }

            const string GuestCookieName = "GuestCartId";
            if (Request.Cookies.TryGetValue(GuestCookieName, out var guestId) && !string.IsNullOrEmpty(guestId))
            {
                return guestId;
            }

            guestId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            };
            Response.Cookies.Append(GuestCookieName, guestId, cookieOptions);
            
            return guestId;
        }
        [HttpGet("admin-user/my")]
        // Removed Authorize policy to allow guests
        public async Task<IActionResult> GetMyCart(CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrGuestId();
            var result = await cartService.GetMyCartAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("admin-user/add")]
        // Removed Authorize policy to allow guests
        public async Task<IActionResult> Add(AddToCartRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrGuestId();
            var result = await cartService.AddToCartAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("admin-user/update-quantity")]
        // Removed Authorize policy to allow guests
        public async Task<IActionResult> UpdateQuantity(UpdateCartItemQuantityRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrGuestId();
            var result = await cartService.UpdateQuantityAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpDelete("admin-user/remove/{productId:guid}")]
        // Removed Authorize policy to allow guests
        public async Task<IActionResult> Remove(Guid productId, CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrGuestId();
            var result = await cartService.RemoveAsync(userId, productId, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpDelete("admin-user/clear")]
        // Removed Authorize policy to allow guests
        public async Task<IActionResult> Clear(CancellationToken cancellationToken)
        {
            var userId = GetUserIdOrGuestId();
            var result = await cartService.ClearAsync(userId, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }

}
