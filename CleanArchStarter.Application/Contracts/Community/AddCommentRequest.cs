using System;

namespace Hook.Application.Contracts.Community;

public class AddCommentRequest
{
    public string CommentText { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
}
