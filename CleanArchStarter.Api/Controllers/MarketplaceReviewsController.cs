using Hook.Application.Contracts.Marketplace.Reviews;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/reviews")]
    public class MarketplaceReviewsController(IMarketplaceReviewService reviewService) : ControllerBase
    {
        [HttpGet("allroles/product/{productId:guid}")]
        [Authorize(Policy = Permissions.MarketplaceReviews_View)]
        public async Task<IActionResult> GetProductReviews(Guid productId, CancellationToken cancellationToken)
        {
            var result = await reviewService.GetProductReviewsAsync(productId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("admin-user/create")]
        [Authorize(Policy = Permissions.MarketplaceReviews_Create)]
        public async Task<IActionResult> Create(CreateMarketplaceReviewRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await reviewService.CreateAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }

}
