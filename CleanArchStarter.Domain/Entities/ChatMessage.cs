using System;
using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class ChatMessage : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid ConversationId { get; set; }
    public virtual ChatConversation Conversation { get; set; } = null!;

    public MessageRole Role { get; set; }
    public string Content { get; set; } = string.Empty;

    public bool FromDatabase { get; set; }
    public string? SourceType { get; set; }
    public string? SourceId { get; set; }
}
