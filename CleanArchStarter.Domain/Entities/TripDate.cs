using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class TripDate : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;

    public Guid TripId { get; set; }
    public virtual Trip Trip { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int AvailableSeats { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
