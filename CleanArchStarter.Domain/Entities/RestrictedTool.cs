using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class RestrictedTool : Auditable
{
    public int Id { get; set; }
    public string ToolName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string Penalty { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
