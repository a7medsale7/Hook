using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Services.Interfaces;

public interface ICommunityService
{
    Task<Result<PostResponse>> CreatePostAsync(string userId, CreatePostRequest request, CancellationToken cancellationToken = default);
    Task<Result<PostResponse>> UpdatePostAsync(Guid postId, string userId, UpdatePostRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletePostAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result> HardDeletePostAsync(Guid postId, CancellationToken cancellationToken = default);
    Task<Result<PostResponse>> GetPostByIdAsync(Guid postId, string currentUserId, CancellationToken cancellationToken = default);
    Task<Result<bool>> ToggleLikeAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result<PostCommentResponse>> AddCommentAsync(Guid postId, string userId, AddCommentRequest request, CancellationToken cancellationToken = default);
    Task<Result<PostCommentResponse>> AddReplyAsync(Guid parentCommentId, string userId, AddReplyRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PostCommentResponse>>> GetPostCommentsAsync(Guid postId, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Result> DeleteCommentAsync(Guid commentId, string userId, CancellationToken cancellationToken = default);
    Task<Result<bool>> ToggleSavePostAsync(Guid postId, string userId, CancellationToken cancellationToken = default);
    Task<Result> FollowUserAsync(string followerId, string followingId, CancellationToken cancellationToken = default);
    Task<Result> UnfollowUserAsync(string followerId, string followingId, CancellationToken cancellationToken = default);
    Task<Result<PostResponse>> SharePostAsync(Guid postId, string userId, SharePostRequest request, CancellationToken cancellationToken = default);
    Task<Result<PostShareInfoResponse>> GetPostShareInfoAsync(Guid postId, string baseUrl, CancellationToken cancellationToken = default);
    Task<Result> ReportPostAsync(Guid postId, string userId, string reason, CancellationToken cancellationToken = default);
}
