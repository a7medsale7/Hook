using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Domain.Enums;

namespace Hook.Application.Services.Interfaces;

public interface IFeedService
{
    Task<Result<IEnumerable<PostResponse>>> GetLatestFeedAsync(string currentUserId, int page, int pageSize, PostCategory? category, string? location, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetAllPostsAsync(string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetTrendingFeedAsync(string currentUserId, int page, int pageSize, PostCategory? category, string? location, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetFollowingFeedAsync(string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetSavedPostsAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetLikedPostsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetSupportedComplaintsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostResponse>>> GetUserPostsAsync(string userId, string currentUserId, int page, int pageSize, CancellationToken cancellationToken = default);
}
