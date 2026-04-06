using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class TripErrors
{
    public static readonly Error NotFound = new("Trip.NotFound", "The specified trip was not found.");
    public static readonly Error DateNotFound = new("Trip.DateNotFound", "The specified trip date was not found.");
    public static readonly Error Unauthorized = new("Trip.Unauthorized", "You are not authorized to manage this trip.");
    public static readonly Error NoBoatAvailable = new("Trip.NoBoatAvailable", "Only approved boat owners with a profile can manage trips.");
    public static readonly Error BoatNotOwned = new("Trip.BoatNotOwned", "The selected boat does not belong to your profile.");
    public static readonly Error BoatNotApproved = new("Trip.BoatNotApproved", "The selected boat must be approved by an administrator.");
    public static readonly Error NoDatesProvided = new("Trip.NoDatesProvided", "At least one date must be provided for the schedule.");
    public static readonly Error InvalidDateRange = new("Trip.InvalidDateRange", "The start date must be before the end date.");
    public static readonly Error DateInPast = new("Trip.DateInPast", "The trip start date cannot be in the past.");
}
