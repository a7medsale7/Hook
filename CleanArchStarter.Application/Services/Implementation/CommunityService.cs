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

public class CommunityService : ICommunityService
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly INotificationService _notificationService;
    private readonly INotificationHubService _notificationHubService;

    public CommunityService(
        ApplicationDbContext context,
        IFileService fileService,
        INotificationService notificationService,
        INotificationHubService notificationHubService)
    {
        _context = context;
        _fileService = fileService;
        _notificationService = notificationService;
        _notificationHubService = notificationHubService;
    }

    public async Task<Result<PostResponse>> CreatePostAsync(string userId, CreatePostRequest request, CancellationToken cancellationToken = default)
    {
        // Validation for Event
        if (request.Category == PostCategory.Event)
        {
            if (request.EventDate == null || request.MaxParticipants == null || request.MaxParticipants <= 0)
            {
                return Result.Failure<PostResponse>(new Error("Community.InvalidEventData", "Event date and max participants are required for event posts."));
            }
        }

        Guid? locationId = null;
        if (!string.IsNullOrEmpty(request.Location))
        {
            var existingLoc = await _context.FishingLocations
                .FirstOrDefaultAsync(l => l.Governorate.ToLower() == request.Location.ToLower() || l.Name.ToLower() == request.Location.ToLower(), cancellationToken);
            
            if (existingLoc != null)
            {
                locationId = existingLoc.Id;
            }
            else
            {
                var newLoc = new FishingLocation
                {
                    Id = Guid.NewGuid(),
                    Name = request.Location,
                    Governorate = request.Location,
                    CreatedById = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _context.FishingLocations.Add(newLoc);
                locationId = newLoc.Id;
            }
        }

        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Category = request.Category,
            Content = request.Content,
            LocationId = locationId,
            LikesCount = 0,
            CommentsCount = 0,
            SharesCount = 0
        };

        // Handle Image Uploads
        if (request.Images != null && request.Images.Any())
        {
            var imageUrls = await _fileService.SaveFilesAsync(request.Images, "posts");
            foreach (var url in imageUrls)
            {
                post.Images.Add(new PostImage
                {
                    Id = Guid.NewGuid(),
                    PostId = post.Id,
                    ImageUrl = url
                });
            }
        }

        _context.Posts.Add(post);

        // Add Event specific details
        if (request.Category == PostCategory.Event)
        {
            var fishingEvent = new FishingEvent
            {
                Id = Guid.NewGuid(),
                PostId = post.Id,
                EventDate = request.EventDate.Value,
                MaxParticipants = request.MaxParticipants.Value,
                CurrentParticipants = 0,
                Status = request.EventDate.Value < DateTime.UtcNow ? EventStatus.Closed : EventStatus.Open
            };
            _context.FishingEvents.Add(fishingEvent);
            post.EventDetails = fishingEvent;
        }

        // Add Complaint specific details
        if (request.Category == PostCategory.Complaint)
        {
            var complaint = new Complaint
            {
                PostId = post.Id,
                Status = ComplaintStatus.Pending,
                SupportCount = 0
            };
            _context.Complaints.Add(complaint);
            post.ComplaintDetails = complaint;
        }

        await _context.SaveChangesAsync(cancellationToken);

        // Notify followers about the new post
        var followerIds = await _context.UserFollows
            .AsNoTracking()
            .Where(f => f.FollowingId == userId)
            .Select(f => f.FollowerId)
            .ToListAsync(cancellationToken);

        foreach (var followerId in followerIds)
        {
            await _notificationService.CreateNotificationAsync(
                followerId,
                userId,
                NotificationType.FollowedUserPost,
                post.Id,
                cancellationToken);
        }

        // Notify admins if it is a new complaint
        if (request.Category == PostCategory.Complaint)
        {
            var targetRoles = await _context.Roles.AsNoTracking()
                .Where(r => r.Name == Hook.Domain.Consts.DefaultRoles.Admin || r.Name == Hook.Domain.Consts.DefaultRoles.CommunityAdmin)
                .Select(r => r.Id)
                .ToListAsync(cancellationToken);

            if (targetRoles.Any())
            {
                var adminUserIds = await _context.UserRoles
                    .AsNoTracking()
                    .Where(ur => targetRoles.Contains(ur.RoleId))
                    .Select(ur => ur.UserId)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                foreach (var adminId in adminUserIds)
                {
                    await _notificationService.CreateNotificationAsync(
                        adminId,
                        userId,
                        NotificationType.NewComplaintForAdmin,
                        post.Id,
                        cancellationToken);
                }
            }
        }

        // Map and return response
        var dbPost = await GetPostWithDetailsAsync(post.Id, userId, cancellationToken);
        return Result.Success(dbPost!);
    }

    public async Task<Result<PostResponse>> UpdatePostAsync(Guid postId, string userId, UpdatePostRequest request, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts
            .Include(p => p.EventDetails)
            .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);

        if (post is null)
        {
            return Result.Failure<PostResponse>(CommunityErrors.PostNotFound);
        }

        if (post.UserId != userId)
        {
            return Result.Failure<PostResponse>(CommunityErrors.UnauthorizedPostAction);
        }

        post.Content = request.Content;
        if (request.Location != null)
        {
            Guid? locationId = null;
            if (!string.IsNullOrEmpty(request.Location))
            {
                var existingLoc = await _context.FishingLocations
                    .FirstOrDefaultAsync(l => l.Governorate.ToLower() == request.Location.ToLower() || l.Name.ToLower() == request.Location.ToLower(), cancellationToken);
                
                if (existingLoc != null)
                {
                    locationId = existingLoc.Id;
                }
                else
                {
                    var newLoc = new FishingLocation
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Location,
                        Governorate = request.Location,
                        CreatedById = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _context.FishingLocations.Add(newLoc);
                    locationId = newLoc.Id;
                }
            }
            post.LocationId = locationId;
        }

        if (post.Category == PostCategory.Event && post.EventDetails != null)
        {
            if (request.EventDate.HasValue)
            {
                post.EventDetails.EventDate = request.EventDate.Value;
                if (post.EventDetails.EventDate < DateTime.UtcNow)
                {
                    post.EventDetails.Status = EventStatus.Closed;
                }
                else if (post.EventDetails.Status == EventStatus.Closed)
                {
                    post.EventDetails.Status = post.EventDetails.CurrentParticipants >= post.EventDetails.MaxParticipants
                        ? EventStatus.Full
                        : EventStatus.Open;
                }
            }

            if (request.MaxParticipants.HasValue)
            {
                var prevMax = post.EventDetails.MaxParticipants;
                post.EventDetails.MaxParticipants = request.MaxParticipants.Value;

                // Requirement: Auto update status when max participants changed (only if not expired)
                if (post.EventDetails.EventDate >= DateTime.UtcNow)
                {
                    if (post.EventDetails.Status == EventStatus.Full && request.MaxParticipants.Value > post.EventDetails.CurrentParticipants)
                    {
                        post.EventDetails.Status = EventStatus.Open;
                    }
                    else if (post.EventDetails.CurrentParticipants >= request.MaxParticipants.Value)
                    {
                        post.EventDetails.Status = EventStatus.Full;
                    }
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        var dbPost = await GetPostWithDetailsAsync(post.Id, userId, cancellationToken);
        return Result.Success(dbPost!);
    }

    public async Task<Result> DeletePostAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        if (post is null)
        {
            return Result.Failure(CommunityErrors.PostNotFound);
        }

        // Verify ownership (or could check for admin roles in controller)
        if (post.UserId != userId)
        {
            // Allow admin to delete
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            var isAdmin = user != null && await _context.UserClaims.AnyAsync(c => c.UserId == userId && c.ClaimValue == "Admin", cancellationToken);
            if (!isAdmin)
            {
                return Result.Failure(CommunityErrors.UnauthorizedPostAction);
            }
        }

        // DbContext save changes automatically handles Soft Delete
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);

        // Broadcast post deletion
        await _notificationHubService.BroadcastPostDeletedAsync(postId);

        return Result.Success();
    }

    public async Task<Result> HardDeletePostAsync(Guid postId, CancellationToken cancellationToken = default)
    {
        var postExists = await _context.Posts.IgnoreQueryFilters().AnyAsync(p => p.Id == postId, cancellationToken);
        if (!postExists)
        {
            return Result.Failure(CommunityErrors.PostNotFound);
        }

        // Delete dependencies first
        await _context.PostImages.IgnoreQueryFilters().Where(i => i.PostId == postId).ExecuteDeleteAsync(cancellationToken);
        await _context.PostLikes.IgnoreQueryFilters().Where(l => l.PostId == postId).ExecuteDeleteAsync(cancellationToken);
        
        await _context.PostComments.IgnoreQueryFilters().Where(c => c.PostId == postId).ExecuteUpdateAsync(s => s.SetProperty(c => c.ParentCommentId, (Guid?)null), cancellationToken);
        await _context.PostComments.IgnoreQueryFilters().Where(c => c.PostId == postId).ExecuteDeleteAsync(cancellationToken);
        
        // Delete event and participants
        var eventIds = await _context.FishingEvents.IgnoreQueryFilters().Where(e => e.PostId == postId).Select(e => e.Id).ToListAsync(cancellationToken);
        if (eventIds.Any())
        {
            await _context.EventParticipants.IgnoreQueryFilters().Where(ep => eventIds.Contains(ep.EventId)).ExecuteDeleteAsync(cancellationToken);
            await _context.FishingEvents.IgnoreQueryFilters().Where(e => eventIds.Contains(e.Id)).ExecuteDeleteAsync(cancellationToken);
        }

        // Delete complaints and supports
        var complaintIds = await _context.Complaints.IgnoreQueryFilters().Where(c => c.PostId == postId).Select(c => c.PostId).ToListAsync(cancellationToken);
        if (complaintIds.Any())
        {
            await _context.ComplaintSupports.IgnoreQueryFilters().Where(cs => complaintIds.Contains(cs.ComplaintId)).ExecuteDeleteAsync(cancellationToken);
            await _context.Complaints.IgnoreQueryFilters().Where(c => complaintIds.Contains(c.PostId)).ExecuteDeleteAsync(cancellationToken);
        }

        await _context.SavedPosts.IgnoreQueryFilters().Where(s => s.PostId == postId).ExecuteDeleteAsync(cancellationToken);
        await _context.PostReports.IgnoreQueryFilters().Where(r => r.PostId == postId).ExecuteDeleteAsync(cancellationToken);
        
        // Nullify OriginalPostId for shared posts
        await _context.Posts.IgnoreQueryFilters().Where(p => p.OriginalPostId == postId).ExecuteUpdateAsync(s => s.SetProperty(p => p.OriginalPostId, (Guid?)null), cancellationToken);
        
        // Finally hard delete the post
        await _context.Posts.IgnoreQueryFilters().Where(p => p.Id == postId).ExecuteDeleteAsync(cancellationToken);

        await _notificationHubService.BroadcastPostDeletedAsync(postId);

        return Result.Success();
    }

    public async Task<Result<PostResponse>> GetPostByIdAsync(Guid postId, string currentUserId, CancellationToken cancellationToken = default)
    {
        var postDto = await GetPostWithDetailsAsync(postId, currentUserId, cancellationToken);
        if (postDto is null)
        {
            return Result.Failure<PostResponse>(CommunityErrors.PostNotFound);
        }

        return Result.Success(postDto);
    }

    public async Task<Result<bool>> ToggleLikeAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        if (post is null)
        {
            return Result.Failure<bool>(CommunityErrors.PostNotFound);
        }

        var existingLike = await _context.PostLikes
            .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId, cancellationToken);

        bool isLiked;
        if (existingLike != null)
        {
            _context.PostLikes.Remove(existingLike);
            post.LikesCount = Math.Max(0, post.LikesCount - 1);
            isLiked = false;
        }
        else
        {
            var like = new PostLike
            {
                PostId = postId,
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };
            _context.PostLikes.Add(like);
            post.LikesCount++;
            isLiked = true;

            // Trigger Notification
            if (post.UserId != userId)
            {
                await _notificationService.CreateNotificationAsync(post.UserId, userId, NotificationType.Like, postId, cancellationToken);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        // Broadcast post interactions update in real-time
        var commentsCount = await _context.PostComments.CountAsync(c => c.PostId == postId && !c.IsDeleted, cancellationToken);
        await _notificationHubService.BroadcastPostInteractionAsync(postId, post.LikesCount, commentsCount, post.SharesCount);

        return Result.Success(isLiked);
    }

    public async Task<Result<PostCommentResponse>> AddCommentAsync(Guid postId, string userId, AddCommentRequest request, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        if (post is null)
        {
            return Result.Failure<PostCommentResponse>(CommunityErrors.PostNotFound);
        }

        var comment = new PostComment
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            UserId = userId,
            CommentText = request.CommentText,
            ParentCommentId = null
        };

        _context.PostComments.Add(comment);
        post.CommentsCount++;
        await _context.SaveChangesAsync(cancellationToken);

        // Fetch commenter details
        var user = await _context.Users.AsNoTracking().FirstAsync(u => u.Id == userId, cancellationToken);

        // Trigger Notification
        if (post.UserId != userId)
        {
            await _notificationService.CreateNotificationAsync(post.UserId, userId, NotificationType.Comment, post.Id, cancellationToken);
        }

        var response = new PostCommentResponse
        {
            Id = comment.Id,
            PostId = comment.PostId,
            UserId = comment.UserId,
            CommenterName = $"{user.FirstName} {user.LastName}",
            CommenterProfilePictureUrl = user.ProfilePictureUrl,
            CommentText = comment.CommentText,
            ParentCommentId = comment.ParentCommentId,
            CreatedOn = comment.CreatedOn
        };

        // Broadcast post interactions update in real-time
        var updatedCommentsCount = await _context.PostComments.CountAsync(c => c.PostId == postId && !c.IsDeleted, cancellationToken);
        await _notificationHubService.BroadcastPostInteractionAsync(postId, post.LikesCount, updatedCommentsCount, post.SharesCount);
        await _notificationHubService.BroadcastCommentAddedAsync(postId, response);

        return Result.Success(response);
    }

    public async Task<Result<PostCommentResponse>> AddReplyAsync(Guid parentCommentId, string userId, AddReplyRequest request, CancellationToken cancellationToken = default)
    {
        var parentComment = await _context.PostComments.FirstOrDefaultAsync(c => c.Id == parentCommentId && !c.IsDeleted, cancellationToken);
        if (parentComment is null)
        {
            return Result.Failure<PostCommentResponse>(CommunityErrors.ParentCommentNotFound);
        }

        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == parentComment.PostId, cancellationToken);
        if (post is null)
        {
            return Result.Failure<PostCommentResponse>(CommunityErrors.PostNotFound);
        }

        var comment = new PostComment
        {
            Id = Guid.NewGuid(),
            PostId = parentComment.PostId,
            UserId = userId,
            CommentText = request.CommentText,
            ParentCommentId = parentCommentId
        };

        _context.PostComments.Add(comment);
        post.CommentsCount++;
        await _context.SaveChangesAsync(cancellationToken);

        // Fetch commenter details
        var user = await _context.Users.AsNoTracking().FirstAsync(u => u.Id == userId, cancellationToken);

        // Trigger Notification
        if (parentComment.UserId != userId)
        {
            await _notificationService.CreateNotificationAsync(parentComment.UserId, userId, NotificationType.Reply, comment.Id, cancellationToken);
        }

        var response = new PostCommentResponse
        {
            Id = comment.Id,
            PostId = comment.PostId,
            UserId = comment.UserId,
            CommenterName = $"{user.FirstName} {user.LastName}",
            CommenterProfilePictureUrl = user.ProfilePictureUrl,
            CommentText = comment.CommentText,
            ParentCommentId = comment.ParentCommentId,
            CreatedOn = comment.CreatedOn
        };

        // Broadcast post interactions update in real-time
        var updatedCommentsCount = await _context.PostComments.CountAsync(c => c.PostId == post.Id && !c.IsDeleted, cancellationToken);
        await _notificationHubService.BroadcastPostInteractionAsync(post.Id, post.LikesCount, updatedCommentsCount, post.SharesCount);
        await _notificationHubService.BroadcastCommentAddedAsync(post.Id, response);

        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<PostCommentResponse>>> GetPostCommentsAsync(Guid postId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId, cancellationToken);
        if (!postExists)
        {
            return Result.Failure<IEnumerable<PostCommentResponse>>(CommunityErrors.PostNotFound);
        }

        // 1. Get paginated root comments
        var rootComments = await _context.PostComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.PostId == postId && c.ParentCommentId == null)
            .OrderByDescending(c => c.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        if (!rootComments.Any())
        {
            return Result.Success<IEnumerable<PostCommentResponse>>(new List<PostCommentResponse>());
        }

        var rootCommentIds = rootComments.Select(c => c.Id).ToList();

        // 2. Get all replies for these root comments
        var replies = await _context.PostComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.PostId == postId && c.ParentCommentId != null && rootCommentIds.Contains(c.ParentCommentId.Value))
            .OrderBy(c => c.CreatedOn)
            .ToListAsync(cancellationToken);

        // 3. Map and build the response structure
        var allComments = rootComments.Concat(replies).ToList();

        var commentResponses = allComments.Select(c => new PostCommentResponse
        {
            Id = c.Id,
            PostId = c.PostId,
            UserId = c.UserId,
            CommenterName = $"{c.User.FirstName} {c.User.LastName}",
            CommenterProfilePictureUrl = c.User.ProfilePictureUrl,
            CommentText = c.CommentText,
            ParentCommentId = c.ParentCommentId,
            CreatedOn = c.CreatedOn,
            Replies = new List<PostCommentResponse>()
        }).ToList();

        var commentMap = commentResponses.ToDictionary(c => c.Id);
        var resultList = new List<PostCommentResponse>();

        foreach (var commentDto in commentResponses)
        {
            if (commentDto.ParentCommentId.HasValue && commentMap.TryGetValue(commentDto.ParentCommentId.Value, out var parentComment))
            {
                parentComment.Replies.Add(commentDto);
            }
            else if (commentDto.ParentCommentId == null)
            {
                resultList.Add(commentDto);
            }
        }

        // Return sorted by CreatedOn descending
        return Result.Success<IEnumerable<PostCommentResponse>>(resultList.OrderByDescending(c => c.CreatedOn));
    }

    public async Task<Result> DeleteCommentAsync(Guid commentId, string userId, CancellationToken cancellationToken = default)
    {
        var comment = await _context.PostComments.FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);
        if (comment is null)
        {
            return Result.Failure(CommunityErrors.CommentNotFound);
        }

        if (comment.UserId != userId)
        {
            return Result.Failure(CommunityErrors.UnauthorizedPostAction);
        }

        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == comment.PostId, cancellationToken);
        if (post != null)
        {
            post.CommentsCount = Math.Max(0, post.CommentsCount - 1);
        }

        _context.PostComments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);

        if (post != null)
        {
            var updatedCommentsCount = await _context.PostComments.CountAsync(c => c.PostId == comment.PostId && !c.IsDeleted, cancellationToken);
            await _notificationHubService.BroadcastPostInteractionAsync(comment.PostId, post.LikesCount, updatedCommentsCount, post.SharesCount);
            await _notificationHubService.BroadcastCommentDeletedAsync(comment.PostId, commentId, updatedCommentsCount);
        }

        return Result.Success();
    }

    public async Task<Result<bool>> ToggleSavePostAsync(Guid postId, string userId, CancellationToken cancellationToken = default)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId, cancellationToken);
        if (!postExists)
        {
            return Result.Failure<bool>(CommunityErrors.PostNotFound);
        }

        var existingSave = await _context.SavedPosts
            .FirstOrDefaultAsync(s => s.PostId == postId && s.UserId == userId, cancellationToken);

        bool isSaved;
        if (existingSave != null)
        {
            _context.SavedPosts.Remove(existingSave);
            isSaved = false;
        }
        else
        {
            var save = new SavedPost
            {
                PostId = postId,
                UserId = userId,
                SavedAt = DateTime.UtcNow
            };
            _context.SavedPosts.Add(save);
            isSaved = true;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(isSaved);
    }

    public async Task<Result> FollowUserAsync(string followerId, string followingId, CancellationToken cancellationToken = default)
    {
        if (followerId == followingId)
        {
            return Result.Failure(CommunityErrors.CannotFollowSelf);
        }

        var targetUserExists = await _context.Users.AnyAsync(u => u.Id == followingId, cancellationToken);
        if (!targetUserExists)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var existingFollow = await _context.UserFollows
            .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId, cancellationToken);

        if (existingFollow)
        {
            return Result.Failure(CommunityErrors.AlreadyFollowing);
        }

        var follow = new UserFollow
        {
            FollowerId = followerId,
            FollowingId = followingId,
            FollowedAt = DateTime.UtcNow
        };

        _context.UserFollows.Add(follow);
        await _context.SaveChangesAsync(cancellationToken);

        // Broadcast user follow updates in real-time
        var targetFollowersCount = await _context.UserFollows.CountAsync(f => f.FollowingId == followingId, cancellationToken);
        var targetFollowingCount = await _context.UserFollows.CountAsync(f => f.FollowerId == followingId, cancellationToken);
        await _notificationHubService.BroadcastUserFollowAsync(followingId, targetFollowersCount, targetFollowingCount);

        var actorFollowersCount = await _context.UserFollows.CountAsync(f => f.FollowingId == followerId, cancellationToken);
        var actorFollowingCount = await _context.UserFollows.CountAsync(f => f.FollowerId == followerId, cancellationToken);
        await _notificationHubService.BroadcastUserFollowAsync(followerId, actorFollowersCount, actorFollowingCount);

        // Trigger Notification
        await _notificationService.CreateNotificationAsync(followingId, followerId, NotificationType.Follow, null, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UnfollowUserAsync(string followerId, string followingId, CancellationToken cancellationToken = default)
    {
        var follow = await _context.UserFollows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId, cancellationToken);

        if (follow is null)
        {
            return Result.Failure(CommunityErrors.FollowNotFound);
        }

        _context.UserFollows.Remove(follow);
        await _context.SaveChangesAsync(cancellationToken);

        // Broadcast user follow updates in real-time
        var targetFollowersCount = await _context.UserFollows.CountAsync(f => f.FollowingId == followingId, cancellationToken);
        var targetFollowingCount = await _context.UserFollows.CountAsync(f => f.FollowerId == followingId, cancellationToken);
        await _notificationHubService.BroadcastUserFollowAsync(followingId, targetFollowersCount, targetFollowingCount);

        var actorFollowersCount = await _context.UserFollows.CountAsync(f => f.FollowingId == followerId, cancellationToken);
        var actorFollowingCount = await _context.UserFollows.CountAsync(f => f.FollowerId == followerId, cancellationToken);
        await _notificationHubService.BroadcastUserFollowAsync(followerId, actorFollowersCount, actorFollowingCount);

        return Result.Success();
    }


    // Helper method to retrieve detailed post response
    private async Task<PostResponse?> GetPostWithDetailsAsync(Guid postId, string currentUserId, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);

        if (post is null) return null;

        var isLiked = await _context.PostLikes.AnyAsync(l => l.PostId == postId && l.UserId == currentUserId, cancellationToken);
        var isSaved = await _context.SavedPosts.AnyAsync(s => s.PostId == postId && s.UserId == currentUserId, cancellationToken);

        var response = new PostResponse
        {
            Id = post.Id,
            UserId = post.UserId,
            AuthorName = $"{post.User.FirstName} {post.User.LastName}",
            AuthorProfilePictureUrl = post.User.ProfilePictureUrl,
            AuthorBio = post.User.Bio,
            Category = post.Category,
            Content = post.Content,
            LocationName = post.Location?.Name,
            Governorate = post.Location?.Governorate,
            OriginalPostId = post.OriginalPostId,
            CreatedOn = post.CreatedOn,
            LikesCount = post.LikesCount,
            CommentsCount = post.CommentsCount,
            SharesCount = post.SharesCount,
            IsLikedByCurrentUser = isLiked,
            IsSavedByCurrentUser = isSaved,
            Images = post.Images.Select(i => i.ImageUrl).ToList()
        };

        // Fetch comments and build reply tree
        var comments = await _context.PostComments
            .AsNoTracking()
            .Include(c => c.User)
            .Where(c => c.PostId == postId)
            .OrderBy(c => c.CreatedOn)
            .ToListAsync(cancellationToken);

        var commentResponses = comments.Select(c => new PostCommentResponse
        {
            Id = c.Id,
            PostId = c.PostId,
            UserId = c.UserId,
            CommenterName = $"{c.User.FirstName} {c.User.LastName}",
            CommenterProfilePictureUrl = c.User.ProfilePictureUrl,
            CommentText = c.CommentText,
            ParentCommentId = c.ParentCommentId,
            CreatedOn = c.CreatedOn,
            Replies = new List<PostCommentResponse>()
        }).ToList();

        var commentMap = commentResponses.ToDictionary(c => c.Id);
        var rootComments = new List<PostCommentResponse>();

        foreach (var commentDto in commentResponses)
        {
            if (commentDto.ParentCommentId.HasValue && commentMap.TryGetValue(commentDto.ParentCommentId.Value, out var parentComment))
            {
                parentComment.Replies.Add(commentDto);
            }
            else
            {
                rootComments.Add(commentDto);
            }
        }

        response.Comments = rootComments;

        if (post.Category == PostCategory.Event && post.EventDetails != null)
        {
            var isJoined = await _context.EventParticipants.AnyAsync(ep => ep.EventId == post.EventDetails.Id && ep.UserId == currentUserId, cancellationToken);
            response.EventDetails = new EventDetailsResponse
            {
                Id = post.EventDetails.Id,
                EventDate = post.EventDetails.EventDate,
                MaxParticipants = post.EventDetails.MaxParticipants,
                CurrentParticipants = post.EventDetails.CurrentParticipants,
                Status = post.EventDetails.EventDate < DateTime.UtcNow ? EventStatus.Closed : post.EventDetails.Status,
                IsJoinedByCurrentUser = isJoined
            };
        }

        if (post.Category == PostCategory.Complaint && post.ComplaintDetails != null)
        {
            var isSupported = await _context.ComplaintSupports.AnyAsync(cs => cs.ComplaintId == postId && cs.UserId == currentUserId, cancellationToken);
            response.ComplaintDetails = new ComplaintDetailsResponse
            {
                Status = post.ComplaintDetails.Status,
                SupportCount = post.ComplaintDetails.SupportCount,
                AdminNotes = post.ComplaintDetails.AdminNotes,
                IsSupportedByCurrentUser = isSupported
            };
        }

        if (post.OriginalPostId.HasValue)
        {
            response.OriginalPost = await GetPostWithDetailsAsync(post.OriginalPostId.Value, currentUserId, cancellationToken);
        }

        return response;
    }

    public async Task<Result<PostResponse>> SharePostAsync(Guid postId, string userId, SharePostRequest request, CancellationToken cancellationToken = default)
    {
        var originalPost = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);

        if (originalPost is null)
            return Result.Failure<PostResponse>(CommunityErrors.PostNotFound);

        // If sharing a post that is already a share, link to the root original post
        var targetOriginalPostId = originalPost.OriginalPostId ?? originalPost.Id;

        // Fetch original post again if it is different to increment its SharesCount
        var actualOriginalPost = originalPost;
        if (originalPost.OriginalPostId.HasValue)
        {
            actualOriginalPost = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == originalPost.OriginalPostId.Value, cancellationToken);
        }

        var newPost = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Category = originalPost.Category,
            Content = request.Content ?? string.Empty,
            OriginalPostId = targetOriginalPostId,
            LocationId = originalPost.LocationId,
            CreatedOn = DateTime.UtcNow
        };

        _context.Posts.Add(newPost);

        if (actualOriginalPost != null)
        {
            actualOriginalPost.SharesCount++;
        }

        await _context.SaveChangesAsync(cancellationToken);

        if (actualOriginalPost != null)
        {
            var originalCommentsCount = await _context.PostComments.CountAsync(c => c.PostId == actualOriginalPost.Id && !c.IsDeleted, cancellationToken);
            await _notificationHubService.BroadcastPostInteractionAsync(actualOriginalPost.Id, actualOriginalPost.LikesCount, originalCommentsCount, actualOriginalPost.SharesCount);
        }

        if (originalPost.UserId != userId)
        {
            await _notificationService.CreateNotificationAsync(
                originalPost.UserId,
                userId,
                NotificationType.PostShared,
                newPost.Id,
                cancellationToken);
        }

        var response = await GetPostByIdAsync(newPost.Id, userId, cancellationToken);
        return response.IsSuccess ? Result.Success(response.Value) : Result.Failure<PostResponse>(response.Error);
    }

    public async Task<Result<PostShareInfoResponse>> GetPostShareInfoAsync(Guid postId, string baseUrl, CancellationToken cancellationToken = default)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId, cancellationToken);
        if (!postExists)
            return Result.Failure<PostShareInfoResponse>(CommunityErrors.PostNotFound);

        var cleanBaseUrl = baseUrl.TrimEnd('/');
        var postUrl = $"{cleanBaseUrl}/posts/{postId}";

        var textToShare = Uri.EscapeDataString($"Check out this post on Hook: {postUrl}");
        var whatsAppUrl = $"https://api.whatsapp.com/send?text={textToShare}";

        var response = new PostShareInfoResponse
        {
            PostId = postId,
            PostUrl = postUrl,
            WhatsAppShareUrl = whatsAppUrl
        };

        return Result.Success(response);
    }

    public async Task<Result> ReportPostAsync(Guid postId, string userId, string reason, CancellationToken cancellationToken = default)
    {
        var post = await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);

        if (post is null)
        {
            return Result.Failure(CommunityErrors.PostNotFound);
        }

        if (post.UserId == userId)
        {
            return Result.Failure(CommunityErrors.CannotReportOwnPost);
        }

        var alreadyReported = await _context.PostReports
            .AnyAsync(r => r.PostId == postId && r.UserId == userId, cancellationToken);

        if (alreadyReported)
        {
            return Result.Failure(CommunityErrors.AlreadyReported);
        }

        var report = new PostReport
        {
            PostId = postId,
            UserId = userId,
            Reason = reason,
            CreatedOn = DateTime.UtcNow
        };

        _context.PostReports.Add(report);
        post.ReportsCount++;

        bool postDeleted = false;
        string? postContentSnippet = null;
        string postAuthorId = post.UserId;

        if (post.ReportsCount >= 70)
        {
            postDeleted = true;
            postContentSnippet = post.Content;
            _context.Posts.Remove(post); // Soft Delete
        }

        await _context.SaveChangesAsync(cancellationToken);

        if (postDeleted)
        {
            var message = $"تم حذف منشورك لتلقيه عدداً كبيراً من البلاغات ({post.ReportsCount} بلاغ). محتوى المنشور: \"{postContentSnippet}\"";
            await _notificationService.CreateNotificationAsync(
                postAuthorId,
                null,
                NotificationType.PostDeletedDueToReports,
                postId,
                message,
                cancellationToken);

            // Broadcast post deletion
            await _notificationHubService.BroadcastPostDeletedAsync(postId);
        }

        return Result.Success();
    }
}
