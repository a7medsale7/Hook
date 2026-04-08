using System;
using System.Collections.Generic;
using Hook.Domain.Enums;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class BoatOwnerProfile : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    // UserId in Identity system is string
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public string NationalIdNumber { get; set; } = string.Empty;
    public string NationalIdPhotoUrl { get; set; } = string.Empty;

    public string BoatLicenseNumber { get; set; } = string.Empty;
    public string BoatLicensePhotoUrl { get; set; } = string.Empty;

    // Social & Payment Info
    public string? InstaPayNumber { get; set; }
    public string? VodafoneCashNumber { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public string? AdminRejectionReason { get; set; }

    public virtual ICollection<Boat> Boats { get; set; } = new HashSet<Boat>();
    public virtual ICollection<Trip> ManagedTrips { get; set; } = new HashSet<Trip>();
}
