using System;

namespace Hook.Application.Contracts.Community;

public class UpdatePostRequest
{
    public string Content { get; set; } = string.Empty;
    public string? Location { get; set; }

    // Event specific fields
    public DateTime? EventDate { get; set; }
    public int? MaxParticipants { get; set; }
}
