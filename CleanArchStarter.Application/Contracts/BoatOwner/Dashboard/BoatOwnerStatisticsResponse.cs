namespace Hook.Application.Contracts.BoatOwner.Dashboard
{
    public record BoatOwnerStatisticsResponse(
        int UpcomingBookings,
        int ActiveTrips,
        decimal AvgRating,
        decimal Earnings
    );
}
