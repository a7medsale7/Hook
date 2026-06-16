using System;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class FishingSeason : Auditable
{
    public int Id { get; set; }
    public string Species { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = string.Empty;
}
