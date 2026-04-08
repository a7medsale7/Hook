using System;

namespace Hook.Application.Contracts.Review;

public class ReviewResponse
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public Guid BookingId { get; set; }
    public string TripName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? UserImage { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedOn { get; set; }
}
