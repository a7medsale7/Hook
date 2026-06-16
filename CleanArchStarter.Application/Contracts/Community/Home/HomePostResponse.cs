using System;

namespace Hook.Application.Contracts.Community.Home;

public class HomePostResponse
{
    public string Id { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public string? OwnerImageUrl { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? PostImageUrl { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public DateTime Date { get; set; }
}
