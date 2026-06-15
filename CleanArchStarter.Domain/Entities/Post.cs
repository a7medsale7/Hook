using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;

namespace Hook.Domain.Entities;

public class Post : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public PostCategory Category { get; set; }
    public string Content { get; set; } = string.Empty;

    public Guid? LocationId { get; set; }
    public virtual FishingLocation? Location { get; set; }

    public Guid? OriginalPostId { get; set; }
    public virtual Post? OriginalPost { get; set; }

    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int SharesCount { get; set; }
    public int ReportsCount { get; set; }

    // Relationships
    public virtual FishingEvent? EventDetails { get; set; }
    public virtual Complaint? ComplaintDetails { get; set; }

    public virtual ICollection<PostImage> Images { get; set; } = new HashSet<PostImage>();
    public virtual ICollection<PostComment> Comments { get; set; } = new HashSet<PostComment>();
    public virtual ICollection<PostLike> Likes { get; set; } = new HashSet<PostLike>();
    public virtual ICollection<Post> SharedPosts { get; set; } = new HashSet<Post>();
}
