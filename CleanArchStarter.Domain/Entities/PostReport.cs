using System;

namespace Hook.Domain.Entities;

public class PostReport
{
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public string Reason { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
