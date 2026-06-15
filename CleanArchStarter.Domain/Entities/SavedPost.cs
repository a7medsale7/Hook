using System;

namespace Hook.Domain.Entities;

public class SavedPost
{
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
}
