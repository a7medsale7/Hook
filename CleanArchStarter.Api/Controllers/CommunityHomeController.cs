using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers;

[Route("api/Community/home")]
[ApiController]
[Authorize]
public class CommunityHomeController(ICommunityHomeService homeService) : ControllerBase
{
    private readonly ICommunityHomeService _homeService = homeService;

    [HttpGet("boats")]
    public async Task<IActionResult> GetHomeBoats(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomeBoatsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("boat-owners")]
    public async Task<IActionResult> GetHomeBoatOwners(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomeBoatOwnersAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("sellers")]
    public async Task<IActionResult> GetHomeSellers(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomeSellersAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetHomeProducts(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomeProductsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("trips")]
    public async Task<IActionResult> GetHomeTrips(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomeTripsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetHomePosts(CancellationToken cancellationToken)
    {
        var result = await _homeService.GetHomePostsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
