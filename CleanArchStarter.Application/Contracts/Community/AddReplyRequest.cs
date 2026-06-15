using System;

namespace Hook.Application.Contracts.Community;

public class AddReplyRequest
{
    public string CommentText { get; set; } = string.Empty;
}
