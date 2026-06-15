using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class Complaint : Auditable
{
    // سنستخدم الـ PostId كـ Primary Key ومفتاح أجنبي في نفس الوقت لتحقيق علاقة 1-to-1 حقيقية
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public ComplaintStatus Status { get; set; } = ComplaintStatus.Pending;
    public int SupportCount { get; set; }
    public string? AdminNotes { get; set; }

    public virtual ICollection<ComplaintSupport> Supports { get; set; } = new HashSet<ComplaintSupport>();
}
