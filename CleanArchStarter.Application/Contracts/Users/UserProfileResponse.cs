using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Users;
public class UserProfileResponse
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Governorate { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePictureUrl { get; set; }

    // Community Stats
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public int SavedPostsCount { get; set; }
    public int LikedPostsCount { get; set; }
    public int SupportedComplaintsCount { get; set; }
    public int PostsCount { get; set; }
    public string RankTitle { get; set; } = string.Empty;
    public bool? IsFollowing { get; set; }
}