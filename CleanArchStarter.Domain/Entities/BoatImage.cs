using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class BoatImage : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;

    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMainImage { get; set; }

    public Guid BoatId { get; set; }
    public virtual Boat Boat { get; set; } = null!;
}
