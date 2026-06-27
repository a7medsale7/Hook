using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Contracts.Community;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunityController(
    ICommunityService communityService,
    IEventService eventService,
    IComplaintService complaintService) : ControllerBase
{
    private readonly ICommunityService _communityService = communityService;
    private readonly IEventService _eventService = eventService;
    private readonly IComplaintService _complaintService = complaintService;

    [HttpPost("posts")]
    [Authorize(Policy = Permissions.Community_Posts_Create)]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.CreatePostAsync(userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("posts/{id}")]
    [Authorize(Policy = Permissions.Community_Posts_Update)]
    public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.UpdatePostAsync(id, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("posts/{id}")]
    [Authorize(Policy = Permissions.Community_Posts_Delete)]
    public async Task<IActionResult> DeletePost(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.DeletePostAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("admin/delete-post/{id}")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> HardDeletePost(Guid id, CancellationToken cancellationToken)
    {
        var result = await _communityService.HardDeletePostAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("posts/{id}")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.GetPostByIdAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("posts/{id}/like")]
    [Authorize(Policy = Permissions.Community_Posts_Like)]
    public async Task<IActionResult> ToggleLike(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.ToggleLikeAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok(new { IsLiked = result.Value }) : BadRequest(result.Error);
    }

    [HttpPost("posts/{id}/save")]
    [Authorize(Policy = Permissions.Community_Posts_Save)]
    public async Task<IActionResult> ToggleSave(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.ToggleSavePostAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok(new { IsSaved = result.Value }) : BadRequest(result.Error);
    }

    [HttpPost("posts/{id}/comments")]
    [Authorize(Policy = Permissions.Community_Comments_Add)]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] AddCommentRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.AddCommentAsync(id, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("posts/{id}/comments")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetPostComments(
        Guid id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.GetPostCommentsAsync(id, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("comments/{commentId}/replies")]
    [Authorize(Policy = Permissions.Community_Comments_Add)]
    public async Task<IActionResult> AddReply(Guid commentId, [FromBody] AddReplyRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.AddReplyAsync(commentId, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("comments/{commentId}")]
    [Authorize(Policy = Permissions.Community_Comments_Delete)]
    public async Task<IActionResult> DeleteComment(Guid commentId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.DeleteCommentAsync(commentId, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("users/{followingId}/follow")]
    [Authorize(Policy = Permissions.Community_User_Follow)]
    public async Task<IActionResult> Follow(string followingId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.FollowUserAsync(userId, followingId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("users/{followingId}/unfollow")]
    [Authorize(Policy = Permissions.Community_User_Follow)]
    public async Task<IActionResult> Unfollow(string followingId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.UnfollowUserAsync(userId, followingId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("events/{postId}/join")]
    [Authorize(Policy = Permissions.Community_Events_Join)]
    public async Task<IActionResult> JoinEvent(Guid postId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _eventService.JoinEventAsync(postId, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("events/{postId}/leave")]
    [Authorize(Policy = Permissions.Community_Events_Join)]
    public async Task<IActionResult> LeaveEvent(Guid postId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _eventService.LeaveEventAsync(postId, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("events/{postId}/participants")]
    [Authorize(Policy = Permissions.Community_Events_Participants_View)]
    public async Task<IActionResult> GetParticipants(Guid postId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _eventService.GetEventParticipantsAsync(postId, userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("posts/{id}/support")]
    [Authorize(Policy = Permissions.Community_Complaints_Support)]
    public async Task<IActionResult> SupportComplaint(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _complaintService.SupportComplaintAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("posts/{id}/report")]
    [Authorize(Policy = Permissions.Community_Posts_Report)]
    public async Task<IActionResult> ReportPost(Guid id, [FromBody] ReportPostRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.ReportPostAsync(id, userId, request.Reason, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }


    [HttpPost("posts/{postId}/share")]
    [Authorize(Policy = Permissions.Community_Posts_Create)]
    public async Task<IActionResult> SharePost(Guid postId, [FromBody] SharePostRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _communityService.SharePostAsync(postId, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("posts/{postId}/share-info")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetShareInfo(Guid postId, CancellationToken cancellationToken)
    {
        var scheme = Request.Scheme;
        var host = Request.Host.ToUriComponent();
        var baseUrl = $"{scheme}://{host}";

        // Detect frontend client base URL dynamically from Referer/Origin headers
        string? clientUrl = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(clientUrl))
        {
            clientUrl = Request.Headers["Origin"].ToString();
        }

        if (!string.IsNullOrEmpty(clientUrl))
        {
            try
            {
                var uri = new Uri(clientUrl);
                baseUrl = $"{uri.Scheme}://{uri.Authority}";
            }
            catch
            {
                // Fallback to backend base url
            }
        }

        var result = await _communityService.GetPostShareInfoAsync(postId, baseUrl, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
