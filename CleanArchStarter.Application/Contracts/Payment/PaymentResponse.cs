using Hook.Domain.Enums;
using System;

namespace Hook.Application.Contracts.Payment;

public record PaymentResponse(
    Guid Id,
    Guid BookingId,
    decimal Amount,
    PaymentStatus Status,
    PaymentMethod Method,
    string? TransactionId,
    string? ReceiptUrl,
    string? AdminNotes,
    DateTime CreatedOn
);

public record PaymentStatsResponse(
    decimal TotalRevenue,
    int PendingVerification,
    int ApprovedPayments,
    int RejectedPayments,
    int RefundedPayments
);
