using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hook.Domain.Consts;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController(INotificationService notificationService) : ControllerBase
{
    private readonly INotificationService _notificationService = notificationService;

    [HttpGet]
    [Authorize(Policy = Permissions.Community_Notifications_View)]
    public async Task<IActionResult> GetNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _notificationService.GetUserNotificationsAsync(userId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("unread/count")]
    [Authorize(Policy = Permissions.Community_Notifications_View)]
    public async Task<IActionResult> GetUnreadNotificationsCount(CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _notificationService.GetUnreadNotificationsCountAsync(userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("unread")]
    [Authorize(Policy = Permissions.Community_Notifications_View)]
    public async Task<IActionResult> GetUnreadNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _notificationService.GetUnreadNotificationsAsync(userId, page, pageSize, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{id}/read")]
    [Authorize(Policy = Permissions.Community_Notifications_View)]
    public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _notificationService.MarkAsReadAsync(id, userId, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
