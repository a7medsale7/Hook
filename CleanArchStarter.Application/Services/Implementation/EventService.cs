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

public class EventService : IEventService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;

    public EventService(ApplicationDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<Result> JoinEventAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var fishingEvent = await _context.FishingEvents
            .Include(e => e.Post)
            .FirstOrDefaultAsync(e => e.PostId == postId, cancellationToken);

        if (fishingEvent is null)
        {
            return Result.Failure(CommunityErrors.PostNotFound);
        }

        if (fishingEvent.EventDate < DateTime.UtcNow)
        {
            if (fishingEvent.Status != EventStatus.Closed)
            {
                fishingEvent.Status = EventStatus.Closed;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Result.Failure(CommunityErrors.EventClosed);
        }

        if (fishingEvent.Status == EventStatus.Full || fishingEvent.CurrentParticipants >= fishingEvent.MaxParticipants)
        {
            return Result.Failure(CommunityErrors.EventFull);
        }

        if (fishingEvent.Status == EventStatus.Closed || fishingEvent.Status == EventStatus.Cancelled)
        {
            return Result.Failure(CommunityErrors.EventClosed);
        }

        var alreadyJoined = await _context.EventParticipants
            .AnyAsync(ep => ep.EventId == fishingEvent.Id && ep.UserId == userId, cancellationToken);

        if (alreadyJoined)
        {
            return Result.Failure(CommunityErrors.AlreadyJoined);
        }

        var participant = new EventParticipant
        {
            Id = Guid.NewGuid(),
            EventId = fishingEvent.Id,
            UserId = userId,
            Status = EventParticipantStatus.Accepted, // انضمام فوري وتلقائي
            JoinedAt = DateTime.UtcNow
        };

        _context.EventParticipants.Add(participant);
        fishingEvent.CurrentParticipants++;

        // تحديث حالة الحدث تلقائياً إلى مكتمل إذا وصل للحد الأقصى
        if (fishingEvent.CurrentParticipants >= fishingEvent.MaxParticipants)
        {
            fishingEvent.Status = EventStatus.Full;
        }

        await _context.SaveChangesAsync(cancellationToken);

        // إرسال إشعار لحظي لصاحب البوست
        if (fishingEvent.Post.UserId != userId)
        {
            await _notificationService.CreateNotificationAsync(
                fishingEvent.Post.UserId, 
                userId, 
                NotificationType.EventJoinRequest, 
                postId, 
                cancellationToken);
        }

        return Result.Success();
    }

    public async Task<Result> LeaveEventAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var fishingEvent = await _context.FishingEvents
            .FirstOrDefaultAsync(e => e.PostId == postId, cancellationToken);

        if (fishingEvent is null)
        {
            return Result.Failure(CommunityErrors.PostNotFound);
        }

        var participant = await _context.EventParticipants
            .FirstOrDefaultAsync(ep => ep.EventId == fishingEvent.Id && ep.UserId == userId, cancellationToken);

        if (participant is null)
        {
            return Result.Failure(CommunityErrors.ParticipantNotFound);
        }

        _context.EventParticipants.Remove(participant);
        fishingEvent.CurrentParticipants = Math.Max(0, fishingEvent.CurrentParticipants - 1);

        // إذا كان الحدث مكتملاً، يتم فتحه مجدداً
        if (fishingEvent.Status == EventStatus.Full && fishingEvent.CurrentParticipants < fishingEvent.MaxParticipants)
        {
            fishingEvent.Status = EventStatus.Open;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<EventParticipantResponse>>> GetEventParticipantsAsync(Guid postId, string currentUserId, CancellationToken cancellationToken = default)
    {
        var fishingEvent = await _context.FishingEvents
            .FirstOrDefaultAsync(e => e.PostId == postId, cancellationToken);

        if (fishingEvent is null)
        {
            return Result.Failure<IEnumerable<EventParticipantResponse>>(CommunityErrors.PostNotFound);
        }

        var participants = await _context.EventParticipants
            .AsNoTracking()
            .Include(ep => ep.User)
            .Where(ep => ep.EventId == fishingEvent.Id)
            .OrderBy(ep => ep.JoinedAt)
            .ToListAsync(cancellationToken);

        var responses = new List<EventParticipantResponse>();

        foreach (var ep in participants)
        {
            bool? isFollowing = null;
            if (ep.UserId != currentUserId)
            {
                isFollowing = await _context.UserFollows
                    .AnyAsync(f => f.FollowerId == currentUserId && f.FollowingId == ep.UserId, cancellationToken);
            }

            responses.Add(new EventParticipantResponse
            {
                UserId = ep.UserId,
                FullName = $"{ep.User.FirstName} {ep.User.LastName}",
                ProfilePictureUrl = ep.User.ProfilePictureUrl,
                JoinedAt = ep.JoinedAt,
                IsFollowing = isFollowing,
                PhoneNumber = ep.User.PhoneNumber
            });
        }

        return Result.Success<IEnumerable<EventParticipantResponse>>(responses);
    }
}
