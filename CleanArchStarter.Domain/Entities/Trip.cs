using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class Trip : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string DetailedDescription { get; set; } = string.Empty;

    // Location Info
    public string LocationName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Trip Details
    public decimal PricePerPerson { get; set; }
    public int MaxParticipants { get; set; }
    public bool IsGuided { get; set; }
    public bool HasEquipmentRental { get; set; }
    public bool HasSnorkeling { get; set; }

    // Foreign Keys
    public Guid BoatId { get; set; }
    public virtual Boat Boat { get; set; } = null!;

    public Guid TripManagerId { get; set; }
    public virtual BoatOwnerProfile TripManager { get; set; } = null!;

    // Navigation Properties
    public virtual ICollection<TripImage> Images { get; set; } = new HashSet<TripImage>();
    public virtual ICollection<TripDate> TripDates { get; set; } = new HashSet<TripDate>();
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
}
