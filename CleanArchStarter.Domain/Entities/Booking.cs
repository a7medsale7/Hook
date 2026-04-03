using System;
using Hook.Domain.Enums;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class Booking : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public Guid TripDateId { get; set; }
    public virtual TripDate TripDate { get; set; } = null!;

    public int NumberOfParticipants { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public string? SpecialRequests { get; set; }

    // Payment (1 to 1)
    public virtual Payment? Payment { get; set; }

    // ✅ التقييمات المكتوبة بناءً على هذا الحجز (عادةً واحدة فقط)
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
}
