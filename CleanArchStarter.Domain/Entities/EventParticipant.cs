using System;
using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class EventParticipant : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid EventId { get; set; }
    public virtual FishingEvent Event { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public EventParticipantStatus Status { get; set; } = EventParticipantStatus.Pending;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
