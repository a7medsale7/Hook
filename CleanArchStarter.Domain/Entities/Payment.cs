using System;
using Hook.Domain.Enums;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class Payment : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;

    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;

    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public PaymentMethod PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    // ✅ لدعم الدفع اليدوي (انستا باي)
    public string? ReceiptImageUrl { get; set; }
    public string? AdminNotes { get; set; }
}
