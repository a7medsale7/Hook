using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class ChatConversation : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public DateTime LastMessageAt { get; set; }
    public string? Summary { get; set; }
    public int MessagesCount { get; set; }
    public bool IsStarred { get; set; } = false;

    public virtual ICollection<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();
}
