using System.Collections.Generic;

namespace Hook.Application.Contracts.Trip;

public record AddTripDatesRequest(
    List<TripDateRequest> Dates
);
