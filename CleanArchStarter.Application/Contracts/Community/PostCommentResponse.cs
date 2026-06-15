using System;
using System.Collections.Generic;

namespace Hook.Application.Contracts.Community;

public class PostCommentResponse
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CommenterName { get; set; } = string.Empty;
    public string? CommenterProfilePictureUrl { get; set; }
    public string CommentText { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
    public DateTime CreatedOn { get; set; }

    public List<PostCommentResponse> Replies { get; set; } = new();
}
