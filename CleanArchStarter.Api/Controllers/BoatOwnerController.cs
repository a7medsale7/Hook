using Hook.Application.Contracts.BoatOwner;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoatOwnerController(IBoatOwnerService boatOwnerService) : ControllerBase
{
    private readonly IBoatOwnerService _boatOwnerService = boatOwnerService;

    [HttpPost("allroles/apply")]
    [Authorize(Policy = Permissions.BoatOwner_Apply)]
    public async Task<IActionResult> Apply([FromForm] ApplyBoatOwnerRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _boatOwnerService.ApplyAsync(userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("boatowner/profile")]
    [Authorize(Policy = Permissions.BoatOwner_ViewProfile)]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _boatOwnerService.GetProfileAsync(userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("admin/pending/GetAll")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_ViewAll)]
    public async Task<IActionResult> GetPendingApplications(CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.GetPendingApplicationsAsync(cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("admin/GetAll")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_ViewAll)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.GetAllAsync(cancellationToken);
        return Ok(result.Value);
    }

    [HttpPost("admin/update-status")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_UpdateStatus)]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateBoatOwnerStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.UpdateStatusAsync(request, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("admin/delete/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.SoftDeleteAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("admin/deleted/GetAll")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_ViewAll)]
    public async Task<IActionResult> GetDeleted(CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.GetDeletedAsync(cancellationToken);
        return Ok(result.Value);
    }

    [HttpPost("admin/restore/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.BoatOwner_Restore)]
    public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
    {
        var result = await _boatOwnerService.RestoreAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
