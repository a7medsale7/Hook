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
    [Route("api/marketplace/analytics")]
    public class MarketplaceAnalyticsController(IMarketplaceAnalyticsService analyticsService) : ControllerBase
    {
        [HttpGet("seller")]
        [Authorize(Policy = Permissions.MarketplaceOrders_Stats)]
        public async Task<IActionResult> Seller(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await analyticsService.GetSellerStatsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.MarketplaceOrders_Stats)]
        public async Task<IActionResult> Admin(CancellationToken cancellationToken)
        {
            var result = await analyticsService.GetAdminStatsAsync(cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
