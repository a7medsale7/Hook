using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class FishingFaq : Auditable
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
