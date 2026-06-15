using System.Threading.Tasks;
using Hook.Application.Contracts.Community;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Hook.Api.Hubs;

public class NotificationHubService : INotificationHubService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHubService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationToUserAsync(string userId, NotificationResponse notification)
    {
        // إرسال الإشعار اللحظي إلى المجموعة المسماة بـ UserId الخاص بالمستلم
        await _hubContext.Clients.Group(userId).SendAsync("ReceiveNotification", notification);
    }

    public async Task BroadcastPostInteractionAsync(System.Guid postId, int likesCount, int commentsCount, int sharesCount)
    {
        await _hubContext.Clients.All.SendAsync("PostInteractionUpdated", new
        {
            PostId = postId,
            LikesCount = likesCount,
            CommentsCount = commentsCount,
            SharesCount = sharesCount
        });
    }

    public async Task BroadcastCommentAddedAsync(System.Guid postId, PostCommentResponse comment)
    {
        await _hubContext.Clients.All.SendAsync("CommentAdded", new
        {
            PostId = postId,
            Comment = comment
        });
    }

    public async Task BroadcastCommentDeletedAsync(System.Guid postId, System.Guid commentId, int updatedCommentsCount)
    {
        await _hubContext.Clients.All.SendAsync("CommentDeleted", new
        {
            PostId = postId,
            CommentId = commentId,
            CommentsCount = updatedCommentsCount
        });
    }

    public async Task BroadcastUserFollowAsync(string userId, int followersCount, int followingCount)
    {
        await _hubContext.Clients.All.SendAsync("UserFollowUpdated", new
        {
            UserId = userId,
            FollowersCount = followersCount,
            FollowingCount = followingCount
        });
    }

    public async Task BroadcastPostDeletedAsync(System.Guid postId)
    {
        await _hubContext.Clients.All.SendAsync("PostDeleted", new
        {
            PostId = postId
        });
    }
}
