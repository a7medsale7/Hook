using System;

namespace Hook.Application.Contracts.Community;

public class PostShareInfoResponse
{
    public Guid PostId { get; set; }
    public string PostUrl { get; set; } = string.Empty;
    public string WhatsAppShareUrl { get; set; } = string.Empty;
}
