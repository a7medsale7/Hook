namespace Hook.Domain.Enums;

public enum NotificationType
{
    Like = 1,
    Comment = 2,
    Reply = 3,
    EventJoinRequest = 4,
    EventAccepted = 5,
    ComplaintSupported = 6,
    PostShared = 7,
    Follow = 8,
    ComplaintEscalated = 9,
    FollowedUserPost = 10,
    ComplaintUnderReview = 11,
    NewComplaintForAdmin = 12,
    PostDeletedDueToReports = 13,
    ComplaintResolved = 14,
    ComplaintRejected = 15
}
