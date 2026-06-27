using System;

namespace Hook.Application.Contracts.BoatOwner.Dashboard
{
    public record UpcomingBookingResponse(
        Guid BookingId,
        string TripTitle,
        DateTime StartDate,
        DateTime EndDate,
        int NumberOfParticipants,
        decimal TotalPrice,
        string Status
    );

    public record ActiveTripResponse(
        Guid TripId,
        string Title,
        string LocationName,
        decimal PricePerPerson,
        int AvailableDatesCount
    );
}
