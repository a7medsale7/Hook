using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class PostImage : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public string ImageUrl { get; set; } = string.Empty;
}
