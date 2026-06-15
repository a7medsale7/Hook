using System;
using System.Collections.Generic;
using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Community;

public class PostResponse
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string? AuthorProfilePictureUrl { get; set; }
    public string? AuthorBio { get; set; }
    
    public PostCategory Category { get; set; }
    public string Content { get; set; } = string.Empty;
    
    public string? LocationName { get; set; }
    public string? Governorate { get; set; }
    
    public Guid? OriginalPostId { get; set; }
    public PostResponse? OriginalPost { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int SharesCount { get; set; }
    
    public bool IsLikedByCurrentUser { get; set; }
    public bool IsSavedByCurrentUser { get; set; }
    
    public List<string> Images { get; set; } = new();

    public List<PostCommentResponse> Comments { get; set; } = new();

    public EventDetailsResponse? EventDetails { get; set; }
    public ComplaintDetailsResponse? ComplaintDetails { get; set; }
}

public class EventDetailsResponse
{
    public Guid Id { get; set; }
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public EventStatus Status { get; set; }
    public bool IsJoinedByCurrentUser { get; set; }
}

public class ComplaintDetailsResponse
{
    public ComplaintStatus Status { get; set; }
    public int SupportCount { get; set; }
    public string? AdminNotes { get; set; }
    public bool IsSupportedByCurrentUser { get; set; }
}
