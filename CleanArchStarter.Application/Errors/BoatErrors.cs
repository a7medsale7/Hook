using Hook.Application.Abstractions.Result;

namespace Hook.Application.Errors;

public static class BoatErrors
{
    public static readonly Error NotFound = 
        new("Boat.NotFound", "The requested boat was not found.");

    public static readonly Error Unauthorized = 
        new("Boat.Unauthorized", "You are not authorized to manage this boat.");

    public static readonly Error NoOwnerProfile = 
        new("Boat.NoOwnerProfile", "You need to create a boat owner profile first.");

    public static readonly Error NotApproved = 
        new("Boat.NotApproved", "Your boat owner profile is not yet approved.");
}
