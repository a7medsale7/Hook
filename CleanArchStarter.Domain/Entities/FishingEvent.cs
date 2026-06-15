using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class FishingEvent : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Open;

    public virtual ICollection<EventParticipant> Participants { get; set; } = new HashSet<EventParticipant>();
}
