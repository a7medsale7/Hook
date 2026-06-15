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

public class ComplaintService : IComplaintService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;

    public ComplaintService(ApplicationDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<Result> SupportComplaintAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var complaint = await _context.Complaints
            .Include(c => c.Post)
            .FirstOrDefaultAsync(c => c.PostId == postId, cancellationToken);

        if (complaint is null)
        {
            return Result.Failure(CommunityErrors.ComplaintNotFound);
        }

        // لا يمكن للمستخدم دعم شكواه الخاصة
        if (complaint.Post.UserId == userId)
        {
            return Result.Failure(CommunityErrors.CannotSupportOwnComplaint);
        }

        var alreadySupported = await _context.ComplaintSupports
            .AnyAsync(s => s.ComplaintId == postId && s.UserId == userId, cancellationToken);

        if (alreadySupported)
        {
            return Result.Failure(CommunityErrors.AlreadySupported);
        }

        var support = new ComplaintSupport
        {
            ComplaintId = postId,
            UserId = userId,
            CreatedOn = DateTime.UtcNow
        };

        _context.ComplaintSupports.Add(support);
        complaint.SupportCount++;

        bool statusChanged = false;
        if (complaint.SupportCount > 50 && complaint.Status == ComplaintStatus.Pending)
        {
            complaint.Status = ComplaintStatus.UnderReview;
            statusChanged = true;
        }

        await _context.SaveChangesAsync(cancellationToken);

        // إرسال إشعار لصاحب الشكوى بأن فلاناً دعم شكواه
        await _notificationService.CreateNotificationAsync(
            complaint.Post.UserId,
            userId,
            NotificationType.ComplaintSupported,
            postId,
            cancellationToken);

        // تصعيد للأدمن وتغيير الحالة لـ UnderReview إذا تخطت 50 دعم
        if (statusChanged)
        {
            // إرسال إشعار لصاحب الشكوى بأن شكواه أصبحت قيد المراجعة
            await _notificationService.CreateNotificationAsync(
                complaint.Post.UserId,
                null,
                NotificationType.ComplaintUnderReview,
                postId,
                cancellationToken);

            var targetRoleNames = new[] { "Admin", "CommunityAdmin" };

            var targetRoleIds = await _context.Roles
                .AsNoTracking()
                .Where(r => targetRoleNames.Contains(r.Name))
                .Select(r => r.Id)
                .ToListAsync(cancellationToken);

            if (targetRoleIds.Any())
            {
                var targetUserIds = await _context.UserRoles
                    .AsNoTracking()
                    .Where(ur => targetRoleIds.Contains(ur.RoleId))
                    .Select(ur => ur.UserId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                foreach (var adminId in targetUserIds)
                {
                    await _notificationService.CreateNotificationAsync(
                        adminId,
                        userId, // Actor is the user who triggered the escalation vote
                        NotificationType.ComplaintEscalated,
                        postId,
                        cancellationToken);
                }
            }
        }

        return Result.Success();
    }

    public async Task<Result<IEnumerable<ComplaintResponse>>> GetComplaintsForAdminAsync(int page, int pageSize, ComplaintStatus? status, string currentUserId, CancellationToken cancellationToken = default)
    {
        var query = _context.Complaints
            .AsNoTracking()
            .Include(c => c.Post)
            .ThenInclude(p => p.User)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(c => c.Status == status.Value);
        }

        var complaints = await query
            .OrderByDescending(c => c.SupportCount)
            .ThenByDescending(c => c.Post.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var responses = new List<ComplaintResponse>();
        foreach (var c in complaints)
        {
            var isSupported = await _context.ComplaintSupports
                .AnyAsync(cs => cs.ComplaintId == c.PostId && cs.UserId == currentUserId, cancellationToken);

            responses.Add(new ComplaintResponse
            {
                PostId = c.PostId,
                Status = c.Status,
                SupportCount = c.SupportCount,
                AdminNotes = c.AdminNotes,
                PostContent = c.Post.Content,
                AuthorName = $"{c.Post.User.FirstName} {c.Post.User.LastName}",
                CreatedOn = c.Post.CreatedOn,
                IsSupportedByCurrentUser = isSupported
            });
        }

        return Result.Success<IEnumerable<ComplaintResponse>>(responses);
    }

    public async Task<Result> ResolveComplaintAsync(Guid postId, string adminId, ResolveComplaintRequest request, CancellationToken cancellationToken = default)
    {
        var complaint = await _context.Complaints
            .Include(c => c.Post)
            .FirstOrDefaultAsync(c => c.PostId == postId, cancellationToken);

        if (complaint is null)
        {
            return Result.Failure(CommunityErrors.ComplaintNotFound);
        }

        var oldStatus = complaint.Status;
        complaint.Status = request.Status;
        complaint.AdminNotes = request.AdminNotes;

        await _context.SaveChangesAsync(cancellationToken);

        // إشعار صاحب الشكوى بأن شكواه تم تحديث حالتها من قبل الإدارة
        if (request.Status == ComplaintStatus.UnderReview && oldStatus != ComplaintStatus.UnderReview)
        {
            await _notificationService.CreateNotificationAsync(
                complaint.Post.UserId,
                adminId,
                NotificationType.ComplaintUnderReview,
                postId,
                cancellationToken);
        }
        else
        {
            await _notificationService.CreateNotificationAsync(
                complaint.Post.UserId,
                adminId, // Actor is the admin
                NotificationType.ComplaintSupported, // نعيد استخدام نوع الدعم أو يمكن إضافته كإشعار
                postId,
                cancellationToken);
        }

        return Result.Success();
    }
}
