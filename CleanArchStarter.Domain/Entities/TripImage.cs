using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class TripImage : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMainImage { get; set; }

    public Guid TripId { get; set; }
    public virtual Trip Trip { get; set; } = null!;
}
