using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class RestrictedLocation : Auditable
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
}
