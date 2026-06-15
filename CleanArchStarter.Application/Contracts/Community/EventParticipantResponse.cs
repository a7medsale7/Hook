using System;

namespace Hook.Application.Contracts.Community;

public class EventParticipantResponse
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTime JoinedAt { get; set; }
    public bool? IsFollowing { get; set; }
    public string? PhoneNumber { get; set; }
}
