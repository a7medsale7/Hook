using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] // Ensure only Admin can access
public class AdminDashboardController(IAdminDashboardService adminDashboardService) : ControllerBase
{
    private readonly IAdminDashboardService _adminDashboardService = adminDashboardService;

    [HttpGet("stats")]
    public async Task<IActionResult> GetDashboardStats(CancellationToken cancellationToken)
    {
        var result = await _adminDashboardService.GetDashboardStatsAsync(cancellationToken);
        
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}
