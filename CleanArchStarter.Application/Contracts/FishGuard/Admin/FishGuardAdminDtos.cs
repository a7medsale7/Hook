using System;

namespace Hook.Application.Contracts.FishGuard.Admin;

// 1. Restricted Locations
public class CreateRestrictedLocationDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateRestrictedLocationDto : CreateRestrictedLocationDto { }

// 2. Restricted Tools
public class CreateRestrictedToolDto
{
    public string ToolName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Penalty { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateRestrictedToolDto : CreateRestrictedToolDto { }

// 3. Fishing Seasons
public class CreateFishingSeasonDto
{
    public string Species { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
}

public class UpdateFishingSeasonDto : CreateFishingSeasonDto { }

// 4. Fishing FAQs
public class CreateFishingFaqDto
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateFishingFaqDto : CreateFishingFaqDto { }
