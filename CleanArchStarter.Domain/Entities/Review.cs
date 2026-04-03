using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class Review : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public Guid TripId { get; set; }
    public virtual Trip Trip { get; set; } = null!;

    // ✅ الربط بالحجز - يضمن أن من يكتب مراجعة قد حجز الرحلة فعلاً
    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;

    public int Rating { get; set; } // 1 to 5
    public string? Comment { get; set; }
}
