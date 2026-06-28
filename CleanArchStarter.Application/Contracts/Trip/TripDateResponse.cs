using System;

namespace Hook.Application.Contracts.Trip;

public record TripDateResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    int AvailableSeats,
    bool IsActive
)
{
    public int DurationDays => (EndDate - StartDate).Days;
    public int DurationHours => (EndDate - StartDate).Hours;
    
    public string RemainingTimeText 
    {
        get
        {
            var remaining = StartDate - DateTime.UtcNow;
            if (remaining.TotalSeconds <= 0) return "Started or ended";
            
            var parts = new System.Collections.Generic.List<string>();
            if (remaining.Days > 0) parts.Add($"{remaining.Days} {(remaining.Days == 1 ? "day" : "days")}");
            if (remaining.Hours > 0) parts.Add($"{remaining.Hours} {(remaining.Hours == 1 ? "hour" : "hours")}");
            if (remaining.Minutes > 0) parts.Add($"{remaining.Minutes} {(remaining.Minutes == 1 ? "minute" : "minutes")}");
            
            return parts.Count > 0 ? string.Join(", ", parts) + " left" : "Less than a minute left";
        }
    }
}
