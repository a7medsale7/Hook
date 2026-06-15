using System;
using System.Collections.Generic;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class PostComment : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser User { get; set; } = null!;

    public string CommentText { get; set; } = string.Empty;

    public Guid? ParentCommentId { get; set; }
    public virtual PostComment? ParentComment { get; set; }

    public virtual ICollection<PostComment> Replies { get; set; } = new HashSet<PostComment>();
}
