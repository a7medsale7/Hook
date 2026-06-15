using System;

namespace Hook.Domain.Entities;

public class UserFollow
{
    public string FollowerId { get; set; } = string.Empty;
    public virtual ApplicationUser Follower { get; set; } = null!;

    public string FollowingId { get; set; } = string.Empty;
    public virtual ApplicationUser Following { get; set; } = null!;

    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
