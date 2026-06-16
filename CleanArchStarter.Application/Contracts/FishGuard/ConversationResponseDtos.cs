using System;

namespace Hook.Application.Contracts.FishGuard;

public class ConversationResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime LastMessageAt { get; set; }
    public int MessagesCount { get; set; }
    public bool IsStarred { get; set; }
}

public class ChatMessageResponseDto
{
    public Guid Id { get; set; }
    public string Role { get; set; } = string.Empty; // "User" or "Assistant"
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
}
