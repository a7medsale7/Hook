using System;
using System.ComponentModel.DataAnnotations;

namespace Hook.Application.Contracts.Booking;

public record CreateBookingRequest(
    [Required] Guid TripDateId,
    [Range(1, 100)] int NumberOfParticipants,
    string? SpecialRequests
);

public record UpdateBookingStatusRequest(
    [Required] Hook.Domain.Enums.BookingStatus Status
);
