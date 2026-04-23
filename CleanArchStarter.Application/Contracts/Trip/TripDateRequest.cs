using System;

namespace Hook.Application.Contracts.Trip;

public record TripDateRequest(
    DateTime StartDate,
    int AvailableSeats
);
