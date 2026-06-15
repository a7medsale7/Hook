using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class FeedService : IFeedService
{
    private readonly ApplicationDbContext _context;

    public FeedService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetLatestFeedAsync(string currentUserId, int page, int pageSize, PostCategory? category, string? location, CancellationToken cancellationToken = default)
    {
        var query = _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .AsQueryable();

        if (category.HasValue)
        {
            query = query.Where(p => p.Category == category.Value);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(p => p.Location != null && (p.Location.Governorate.ToLower() == location.ToLower() || p.Location.Name.ToLower() == location.ToLower()));
        }

        var posts = await query
            .OrderByDescending(p => p.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetTrendingFeedAsync(string currentUserId, int page, int pageSize, PostCategory? category, string? location, CancellationToken cancellationToken = default)
    {
        var query = _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .AsQueryable();

        if (category.HasValue)
        {
            query = query.Where(p => p.Category == category.Value);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(p => p.Location != null && (p.Location.Governorate.ToLower() == location.ToLower() || p.Location.Name.ToLower() == location.ToLower()));
        }

        // Trending Score = Likes + Comments + Shares
        var posts = await query
            .OrderByDescending(p => p.LikesCount + p.CommentsCount + p.SharesCount)
            .ThenByDescending(p => p.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetFollowingFeedAsync(string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var followingIds = await _context.UserFollows
            .AsNoTracking()
            .Where(f => f.FollowerId == currentUserId)
            .Select(f => f.FollowingId)
            .ToListAsync(cancellationToken);

        if (!followingIds.Any())
        {
            return Result.Success<IEnumerable<PostResponse>>(new List<PostResponse>());
        }

        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .Where(p => followingIds.Contains(p.UserId))
            .OrderBy(p => Guid.NewGuid())
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetSavedPostsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var savedPostIdsQuery = _context.SavedPosts
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.SavedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var savedPosts = await savedPostIdsQuery.ToListAsync(cancellationToken);
        if (!savedPosts.Any())
        {
            return Result.Success<IEnumerable<PostResponse>>(new List<PostResponse>());
        }

        var postIds = savedPosts.Select(s => s.PostId).ToList();

        var postsMap = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .Where(p => postIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        // Keep the saved order
        var posts = postIds
            .Where(id => postsMap.ContainsKey(id))
            .Select(id => postsMap[id])
            .ToList();

        var response = await MapPostsAsync(posts, userId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    private async Task<List<PostResponse>> MapPostsAsync(List<Post> posts, string currentUserId, CancellationToken cancellationToken)
    {
        if (!posts.Any()) return new List<PostResponse>();

        var postIds = posts.Select(p => p.Id).ToList();

        // Batch fetch likes and saves for current user to avoid N+1 queries
        var userLikes = await _context.PostLikes
            .AsNoTracking()
            .Where(l => l.UserId == currentUserId && postIds.Contains(l.PostId))
            .Select(l => l.PostId)
            .ToListAsync(cancellationToken);

        var userSaves = await _context.SavedPosts
            .AsNoTracking()
            .Where(s => s.UserId == currentUserId && postIds.Contains(s.PostId))
            .Select(s => s.PostId)
            .ToListAsync(cancellationToken);

        // Event Joins
        var eventIds = posts
            .Where(p => p.Category == PostCategory.Event && p.EventDetails != null)
            .Select(p => p.EventDetails.Id)
            .ToList();

        var userJoins = await _context.EventParticipants
            .AsNoTracking()
            .Where(ep => ep.UserId == currentUserId && eventIds.Contains(ep.EventId))
            .Select(ep => ep.EventId)
            .ToListAsync(cancellationToken);

        // Complaint Supports
        var userSupports = await _context.ComplaintSupports
            .AsNoTracking()
            .Where(cs => cs.UserId == currentUserId && postIds.Contains(cs.ComplaintId))
            .Select(cs => cs.ComplaintId)
            .ToListAsync(cancellationToken);

        var responses = new List<PostResponse>();

        foreach (var post in posts)
        {
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
                IsLikedByCurrentUser = userLikes.Contains(post.Id),
                IsSavedByCurrentUser = userSaves.Contains(post.Id),
                Images = post.Images.Select(i => i.ImageUrl).ToList()
            };

            if (post.Category == PostCategory.Event && post.EventDetails != null)
            {
                response.EventDetails = new EventDetailsResponse
                {
                    Id = post.EventDetails.Id,
                    EventDate = post.EventDetails.EventDate,
                    MaxParticipants = post.EventDetails.MaxParticipants,
                    CurrentParticipants = post.EventDetails.CurrentParticipants,
                    Status = post.EventDetails.EventDate < DateTime.UtcNow ? EventStatus.Closed : post.EventDetails.Status,
                    IsJoinedByCurrentUser = userJoins.Contains(post.EventDetails.Id)
                };
            }

            if (post.Category == PostCategory.Complaint && post.ComplaintDetails != null)
            {
                response.ComplaintDetails = new ComplaintDetailsResponse
                {
                    Status = post.ComplaintDetails.Status,
                    SupportCount = post.ComplaintDetails.SupportCount,
                    AdminNotes = post.ComplaintDetails.AdminNotes,
                    IsSupportedByCurrentUser = userSupports.Contains(post.Id)
                };
            }

            if (post.OriginalPostId.HasValue && post.OriginalPost != null)
            {
                response.OriginalPost = new PostResponse
                {
                    Id = post.OriginalPost.Id,
                    UserId = post.OriginalPost.UserId,
                    AuthorName = $"{post.OriginalPost.User?.FirstName} {post.OriginalPost.User?.LastName}",
                    AuthorProfilePictureUrl = post.OriginalPost.User?.ProfilePictureUrl,
                    AuthorBio = post.OriginalPost.User?.Bio,
                    Category = post.OriginalPost.Category,
                    Content = post.OriginalPost.Content,
                    LocationName = post.OriginalPost.Location?.Name,
                    Governorate = post.OriginalPost.Location?.Governorate,
                    CreatedOn = post.OriginalPost.CreatedOn,
                    LikesCount = post.OriginalPost.LikesCount,
                    CommentsCount = post.OriginalPost.CommentsCount,
                    SharesCount = post.OriginalPost.SharesCount,
                    IsLikedByCurrentUser = userLikes.Contains(post.OriginalPostId.Value),
                    IsSavedByCurrentUser = userSaves.Contains(post.OriginalPostId.Value),
                    Images = post.OriginalPost.Images.Select(i => i.ImageUrl).ToList()
                };
            }

            responses.Add(response);
        }

        return responses;
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetLikedPostsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var likedPostIdsQuery = _context.PostLikes
            .AsNoTracking()
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var likedPosts = await likedPostIdsQuery.ToListAsync(cancellationToken);
        if (!likedPosts.Any())
        {
            return Result.Success<IEnumerable<PostResponse>>(new List<PostResponse>());
        }

        var postIds = likedPosts.Select(l => l.PostId).ToList();

        var postsMap = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .Where(p => postIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        var posts = postIds
            .Where(id => postsMap.ContainsKey(id))
            .Select(id => postsMap[id])
            .ToList();

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetSupportedComplaintsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var supportedComplaintIdsQuery = _context.ComplaintSupports
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var supportedComplaints = await supportedComplaintIdsQuery.ToListAsync(cancellationToken);
        if (!supportedComplaints.Any())
        {
            return Result.Success<IEnumerable<PostResponse>>(new List<PostResponse>());
        }

        var postIds = supportedComplaints.Select(s => s.ComplaintId).ToList();

        var postsMap = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .Where(p => postIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        var posts = postIds
            .Where(id => postsMap.ContainsKey(id))
            .Select(id => postsMap[id])
            .ToList();

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }

    public async Task<Result<IEnumerable<PostResponse>>> GetUserPostsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var posts = await _context.Posts
            .AsNoTracking()
            .Include(p => p.User)
            .Include(p => p.Location)
            .Include(p => p.Images)
            .Include(p => p.EventDetails)
            .Include(p => p.ComplaintDetails)
            .Include(p => p.OriginalPost)
            .ThenInclude(op => op.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedOn)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var response = await MapPostsAsync(posts, currentUserId, cancellationToken);
        return Result.Success<IEnumerable<PostResponse>>(response);
    }
}
