using System.Threading.Tasks;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Services.Interfaces;

public interface INotificationHubService
{
    Task SendNotificationToUserAsync(string userId, NotificationResponse notification);
    Task BroadcastPostInteractionAsync(System.Guid postId, int likesCount, int commentsCount, int sharesCount);
    Task BroadcastCommentAddedAsync(System.Guid postId, PostCommentResponse comment);
    Task BroadcastCommentDeletedAsync(System.Guid postId, System.Guid commentId, int updatedCommentsCount);
    Task BroadcastUserFollowAsync(string userId, int followersCount, int followingCount);
    Task BroadcastPostDeletedAsync(System.Guid postId);
}
