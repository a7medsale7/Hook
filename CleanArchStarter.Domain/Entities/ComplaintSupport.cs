using System;

namespace Hook.Domain.Entities;

public class ComplaintSupport
{
    public Guid ComplaintId { get; set; }
    public virtual Complaint Complaint { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
