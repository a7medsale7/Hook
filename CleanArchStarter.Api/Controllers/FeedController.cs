using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hook.Domain.Consts;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FeedController(IFeedService feedService) : ControllerBase
{
    private readonly IFeedService _feedService = feedService;

    [HttpGet("latest")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetLatest(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] PostCategory? category = null,
        [FromQuery] string? location = null,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _feedService.GetLatestFeedAsync(userId, page, pageSize, category, location, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("trending")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetTrending(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] PostCategory? category = null,
        [FromQuery] string? location = null,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _feedService.GetTrendingFeedAsync(userId, page, pageSize, category, location, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("following")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetFollowing(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _feedService.GetFollowingFeedAsync(userId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("saved")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetSaved(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _feedService.GetSavedPostsAsync(userId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("liked")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetLiked(
        [FromQuery] string? userId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();

        var targetUserId = string.IsNullOrWhiteSpace(userId) ? currentUserId : userId;

        var result = await _feedService.GetLikedPostsAsync(targetUserId, currentUserId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("supported-complaints")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetSupportedComplaints(
        [FromQuery] string? userId = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();

        var targetUserId = string.IsNullOrWhiteSpace(userId) ? currentUserId : userId;

        var result = await _feedService.GetSupportedComplaintsAsync(targetUserId, currentUserId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my-posts")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetMyPosts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _feedService.GetUserPostsAsync(userId, userId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Policy = Permissions.Community_Feed_View)]
    public async Task<IActionResult> GetUserPosts(
        [FromRoute] string userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();

        var result = await _feedService.GetUserPostsAsync(userId, currentUserId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
