using Hook.Application.Contracts.Boat;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoatsController(IBoatService boatService) : ControllerBase
{
    private readonly IBoatService _boatService = boatService;

    [HttpPost("boatowner/create")]
    [Authorize(Policy = Permissions.Boats_Create)]
    public async Task<IActionResult> Create([FromForm] CreateBoatRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _boatService.CreateAsync(userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("allroles/{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _boatService.GetByIdAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("allroles/GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _boatService.GetAllAsync(cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("user-boatowner/my-boats")]
    [Authorize(Policy = Permissions.Boats_View)]
    public async Task<IActionResult> GetMyBoats(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _boatService.GetMyBoatsAsync(userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("boatowner/update/{id}")]
    [Authorize(Policy = Permissions.Boats_Update)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateBoatRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var result = await _boatService.UpdateAsync(id, userId, request, isAdmin, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("boatowner/delete/{id}")]
    [Authorize(Policy = Permissions.Boats_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var result = await _boatService.SoftDeleteAsync(id, userId, isAdmin, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("admin/restore/{id}")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _boatService.RestoreAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
