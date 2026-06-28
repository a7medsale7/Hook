using System;

namespace Hook.Application.Contracts.Trip;

public record TripDateRequest(
    DateTime StartDate,
    DateTime EndDate,
    int AvailableSeats
);
