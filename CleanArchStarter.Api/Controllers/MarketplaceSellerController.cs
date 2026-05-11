using Hook.Application.Contracts.Marketplace.Seller;
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
    [Route("api/marketplace/seller")]
    public class MarketplaceSellerController(IMarketplaceSellerService sellerService) : ControllerBase
    {
        [HttpPost("admin-user/list-item")]
        [Authorize(Policy = Permissions.Seller_Apply)]
        public async Task<IActionResult> ListItem([FromForm] CreateListingRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await sellerService.CreateListingRequestAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin-user/my-requests")]
        [Authorize(Policy = Permissions.Seller_ViewProfile)]
        public async Task<IActionResult> MyRequests(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await sellerService.GetMyListingRequestsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin/pending")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceApprovals_View)]
        public async Task<IActionResult> Pending(CancellationToken cancellationToken)
        {
            var result = await sellerService.GetPendingListingRequestsAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("admin/update-status")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceApprovals_Update)]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateListingRequestStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await sellerService.UpdateListingRequestStatusAsync(request, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }


}
