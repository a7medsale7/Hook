using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class BookingErrors
{
    public static readonly Error NotFound = new("Booking.NotFound", "The requested booking was not found.");
    public static readonly Error Unauthorized = new("Booking.Unauthorized", "You are not authorized to view or manage this booking.");
    public static readonly Error InsufficientSeats = new("Booking.InsufficientSeats", "Not enough seats available for this trip date.");
    public static readonly Error TripDateInactive = new("Booking.TripDateInactive", "This trip date is no longer active for booking.");
    public static readonly Error TripDatePassed = new("Booking.TripDatePassed", "Cannot book for a trip date that has already passed.");
    public static readonly Error AlreadyCancelled = new("Booking.AlreadyCancelled", "This booking is already cancelled.");
    public static readonly Error AlreadyCompleted = new("Booking.AlreadyCompleted", "This booking is already marked as completed.");
    public static readonly Error AlreadyBooked = new("Booking.AlreadyBooked", "You have already booked for this trip date.");
}
