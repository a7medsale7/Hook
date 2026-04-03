using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class Boat : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }

    public Guid OwnerProfileId { get; set; }
    public virtual BoatOwnerProfile OwnerProfile { get; set; } = null!;

    public virtual ICollection<BoatImage> Images { get; set; } = new HashSet<BoatImage>();
    public virtual ICollection<Trip> Trips { get; set; } = new HashSet<Trip>();
}
