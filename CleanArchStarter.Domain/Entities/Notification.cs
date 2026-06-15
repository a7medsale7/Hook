using System;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public NotificationType Type { get; set; }
    public Guid? ReferenceId { get; set; }
    public string? ActorUserId { get; set; }
    public virtual ApplicationUser? ActorUser { get; set; }
    public bool IsRead { get; set; } = false;
    public string? Message { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
