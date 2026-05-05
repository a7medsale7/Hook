using System;
using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Booking;

public record BookingResponse(
    Guid Id,
    string TripTitle,
    string? MainImageUrl,
    DateTime StartDate,
    DateTime EndDate,
    string BoatName,
    int NumberOfParticipants,
    decimal TotalPrice,
    BookingStatus Status,
    string? SpecialRequests,
    string? UserFullName, // For owner/admin view
    string? UserPhoneNumber,
    string? UserEmail,
    BookingPaymentInfo? Payment
);

public record BookingPaymentInfo(
    Guid Id,
    decimal Amount,
    PaymentStatus Status,
    PaymentMethod Method,
    string? TransactionId,
    string? ReceiptImageUrl
);
