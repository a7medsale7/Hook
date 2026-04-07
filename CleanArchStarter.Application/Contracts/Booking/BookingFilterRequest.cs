using Hook.Domain.Enums;
using System;

namespace Hook.Application.Contracts.Booking;

public record BookingFilterRequest(
    BookingStatus? Status = null,
    string? Location = null,
    DateTime? Date = null
);
