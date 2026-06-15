using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class FishingLocation : Auditable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Governorate { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
