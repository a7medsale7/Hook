using System;

namespace Hook.Application.Contracts.Trip;

public record TripDateResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    int AvailableSeats,
    bool IsActive
);
