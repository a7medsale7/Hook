using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationHubService _hubService;

    public NotificationService(ApplicationDbContext context, INotificationHubService hubService)
    {
        _context = context;
        _hubService = hubService;
    }

    public async Task<Result<IEnumerable<NotificationResponse>>> GetUserNotificationsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var notifications = await _context.Notifications
            .AsNoTracking()
            .Include(n => n.ActorUser)
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var response = notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            UserId = n.UserId,
            Type = n.Type,
            ReferenceId = n.ReferenceId,
            IsRead = n.IsRead,
            CreatedOn = n.CreatedOn,
            ActorName = n.ActorUser != null ? $"{n.ActorUser.FirstName} {n.ActorUser.LastName}" : "System",
            ActorProfilePictureUrl = n.ActorUser?.ProfilePictureUrl,
            Message = n.Message ?? GetNotificationMessage(n.Type, n.ActorUser != null ? $"{n.ActorUser.FirstName} {n.ActorUser.LastName}" : "System")
        });

        return Result.Success(response);
    }

    public async Task<Result> MarkAsReadAsync(Guid notificationId, string userId, CancellationToken cancellationToken = default)
    {
        var notification = await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId, cancellationToken);

        if (notification is null)
        {
            return Result.Failure(CommunityErrors.NotificationNotFound);
        }

        notification.IsRead = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task CreateNotificationAsync(string userId, string? actorUserId, NotificationType type, Guid? referenceId, CancellationToken cancellationToken = default)
    {
        await CreateNotificationAsync(userId, actorUserId, type, referenceId, null, cancellationToken);
    }

    public async Task CreateNotificationAsync(string userId, string? actorUserId, NotificationType type, Guid? referenceId, string? message, CancellationToken cancellationToken = default)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ActorUserId = actorUserId,
            Type = type,
            ReferenceId = referenceId,
            IsRead = false,
            Message = message,
            CreatedOn = DateTime.UtcNow
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);

        // Load Actor Details to push via Hub
        var actor = actorUserId != null
            ? await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == actorUserId, cancellationToken)
            : null;

        var actorName = actor != null ? $"{actor.FirstName} {actor.LastName}" : "System";

        var responseDto = new NotificationResponse
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Type = notification.Type,
            ReferenceId = notification.ReferenceId,
            IsRead = notification.IsRead,
            CreatedOn = notification.CreatedOn,
            ActorName = actorName,
            ActorProfilePictureUrl = actor?.ProfilePictureUrl,
            Message = message ?? GetNotificationMessage(notification.Type, actorName)
        };

        // Push real-time notification via hub
        await _hubService.SendNotificationToUserAsync(userId, responseDto);
    }

    private static string GetNotificationMessage(NotificationType type, string actorName)
    {
        return type switch
        {
            NotificationType.Like => $"{actorName} liked your post.",
            NotificationType.Comment => $"{actorName} commented on your post.",
            NotificationType.Reply => $"{actorName} replied to your comment.",
            NotificationType.EventJoinRequest => $"{actorName} joined your fishing event.",
            NotificationType.EventAccepted => $"You are now accepted in the event by {actorName}.",
            NotificationType.ComplaintSupported => $"{actorName} supported your complaint.",
            NotificationType.ComplaintEscalated => "A complaint has been escalated to admins (exceeded 50 supports).",
            NotificationType.PostShared => $"{actorName} shared your post.",
            NotificationType.Follow => $"{actorName} started following you.",
            NotificationType.FollowedUserPost => $"{actorName} published a new post.",
            NotificationType.ComplaintUnderReview => "Your complaint is now under review by the administration.",
            NotificationType.NewComplaintForAdmin => "A new complaint has been posted.",
            NotificationType.PostDeletedDueToReports => "Your post was deleted due to receiving too many reports.",
            _ => "You have a new notification."
        };
    }
}
