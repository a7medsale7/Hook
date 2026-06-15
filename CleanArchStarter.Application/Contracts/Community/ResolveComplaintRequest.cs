using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Community;

public class ResolveComplaintRequest
{
    public ComplaintStatus Status { get; set; } = ComplaintStatus.Resolved;
    public string? AdminNotes { get; set; }
}
