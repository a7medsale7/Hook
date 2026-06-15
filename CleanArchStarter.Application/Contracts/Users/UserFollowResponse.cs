namespace Hook.Application.Contracts.Users;

public class UserFollowResponse
{
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsFollowing { get; set; }
}
