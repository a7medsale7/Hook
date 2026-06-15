using System;
using Hook.Domain.Enums;

namespace Hook.Application.Contracts.Community;

public class ComplaintResponse
{
    public Guid PostId { get; set; }
    public ComplaintStatus Status { get; set; }
    public int SupportCount { get; set; }
    public string? AdminNotes { get; set; }

    public string PostContent { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public bool IsSupportedByCurrentUser { get; set; }
}
