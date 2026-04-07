namespace Hook.Application.Contracts.Booking;

public record BookingStatsResponse(
    int TotalBookings,
    int PendingBookings,
    int ApprovedBookings,
    int RejectedBookings,
    int CompletedBookings,
    int CancelledBookings,
    decimal TotalRevenue
);
