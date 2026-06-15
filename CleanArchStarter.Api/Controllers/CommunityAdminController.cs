using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.Community;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Hook.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers;

[Route("api/admin/community")]
[ApiController]
public class CommunityAdminController(IComplaintService complaintService) : ControllerBase
{
    private readonly IComplaintService _complaintService = complaintService;

    [HttpGet("complaints")]
    [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.CommunityAdmin}", Policy = Permissions.CommunityAdmin_Complaints_View)]
    public async Task<IActionResult> GetComplaints(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] ComplaintStatus? status = null,
        CancellationToken cancellationToken = default)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId is null) return Unauthorized();

        var result = await _complaintService.GetComplaintsForAdminAsync(page, pageSize, status, adminId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("complaints/under-review")]
    [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.CommunityAdmin}", Policy = Permissions.CommunityAdmin_Complaints_View)]
    public async Task<IActionResult> GetUnderReviewComplaints(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId is null) return Unauthorized();

        var result = await _complaintService.GetComplaintsForAdminAsync(page, pageSize, ComplaintStatus.UnderReview, adminId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("complaints/{postId}/resolve")]
    [Authorize(Roles = $"{DefaultRoles.Admin},{DefaultRoles.CommunityAdmin}", Policy = Permissions.CommunityAdmin_Complaints_Resolve)]
    public async Task<IActionResult> ResolveComplaint(Guid postId, [FromBody] ResolveComplaintRequest request, CancellationToken cancellationToken)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminId is null) return Unauthorized();

        var result = await _complaintService.ResolveComplaintAsync(postId, adminId, request, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
