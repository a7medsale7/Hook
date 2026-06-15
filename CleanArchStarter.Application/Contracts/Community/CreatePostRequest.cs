using System;
using System.Collections.Generic;
using Hook.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Hook.Application.Contracts.Community;

public class CreatePostRequest
{
    public string Content { get; set; } = string.Empty;
    public string? Location { get; set; }
    public PostCategory Category { get; set; }

    // Event specific fields
    public DateTime? EventDate { get; set; }
    public int? MaxParticipants { get; set; }

    // Uploaded images
    public List<IFormFile>? Images { get; set; }
}
