using System;
using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Community;

public class NotificationResponse
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedOn { get; set; }
    
    // Additional client helper fields
    public string Message { get; set; } = string.Empty;
    public string? ActorName { get; set; }
    public string? ActorProfilePictureUrl { get; set; }
}
