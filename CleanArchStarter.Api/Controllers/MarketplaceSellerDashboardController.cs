using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/marketplace/seller/dashboard")]
    public class MarketplaceSellerDashboardController(ISellerDashboardService dashboardService) : ControllerBase
    {
        private readonly ISellerDashboardService _dashboardService = dashboardService;

        [HttpGet("statistics")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> GetStatistics(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetStatisticsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("recent-orders")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> GetRecentOrders([FromQuery] int count = 20, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetRecentOrdersAsync(userId, count, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("recent-reviews")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> GetRecentReviews([FromQuery] int count = 20, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetRecentReviewsAsync(userId, count, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("sales-over-time")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> GetSalesOverTime(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetSalesOverTimeAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("top-products")]
        [Authorize(Policy = Permissions.MarketplaceProducts_View)]
        public async Task<IActionResult> GetTopSellingProducts([FromQuery] int count = 5, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetTopSellingProductsAsync(userId, count, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
