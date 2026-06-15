using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class CommunityErrors
{
    public static readonly Error PostNotFound =
        new("Community.PostNotFound", "The requested post was not found.");

    public static readonly Error UnauthorizedPostAction =
        new("Community.UnauthorizedPostAction", "You are not authorized to perform this action on the post.");

    public static readonly Error CommentNotFound =
        new("Community.CommentNotFound", "The requested comment was not found.");

    public static readonly Error ParentCommentNotFound =
        new("Community.ParentCommentNotFound", "The parent comment to reply to was not found.");

    public static readonly Error AlreadyJoined =
        new("Community.AlreadyJoined", "You have already joined this event.");

    public static readonly Error EventFull =
        new("Community.EventFull", "This event has reached its maximum capacity.");

    public static readonly Error EventClosed =
        new("Community.EventClosed", "This event is closed or cancelled.");

    public static readonly Error ParticipantNotFound =
        new("Community.ParticipantNotFound", "You are not a participant in this event.");

    public static readonly Error CannotFollowSelf =
        new("Community.CannotFollowSelf", "You cannot follow yourself.");

    public static readonly Error AlreadyFollowing =
        new("Community.AlreadyFollowing", "You are already following this user.");

    public static readonly Error FollowNotFound =
        new("Community.FollowNotFound", "You are not following this user.");

    public static readonly Error ComplaintNotFound =
        new("Community.ComplaintNotFound", "The requested complaint was not found.");

    public static readonly Error AlreadySupported =
        new("Community.AlreadySupported", "You have already supported this complaint.");

    public static readonly Error CannotSupportOwnComplaint =
        new("Community.CannotSupportOwnComplaint", "You cannot support your own complaint.");

    public static readonly Error NotificationNotFound =
        new("Community.NotificationNotFound", "The requested notification was not found.");

    public static readonly Error CannotReportOwnPost =
        new("Community.CannotReportOwnPost", "You cannot report your own post.");

    public static readonly Error AlreadyReported =
        new("Community.AlreadyReported", "You have already reported this post.");
}
