using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Api.Controllers
{
    [ApiController]
    [Route("api/boat-owner/dashboard")]
    public class BoatOwnerDashboardController(IBoatOwnerDashboardService dashboardService) : ControllerBase
    {
        private readonly IBoatOwnerDashboardService _dashboardService = dashboardService;

        [HttpGet("statistics")]
        [Authorize(Policy = Permissions.BoatOwner_ViewProfile)]
        public async Task<IActionResult> GetStatistics(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetStatisticsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("upcoming-bookings")]
        [Authorize(Policy = Permissions.BoatOwner_ViewProfile)]
        public async Task<IActionResult> GetUpcomingBookings(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetUpcomingBookingsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("active-trips")]
        [Authorize(Policy = Permissions.BoatOwner_ViewProfile)]
        public async Task<IActionResult> GetActiveTrips(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _dashboardService.GetActiveTripsAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
