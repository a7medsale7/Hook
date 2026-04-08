using System;
using System.ComponentModel.DataAnnotations;

namespace Hook.Application.Contracts.Review;

public class CreateReviewRequest
{
    [Required]
    public Guid BookingId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    public string? Comment { get; set; }
}

public class UpdateReviewRequest
{
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    public string? Comment { get; set; }
}
