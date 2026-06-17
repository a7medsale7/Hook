using System;
using System.Collections.Generic;

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
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Material { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public double? MinMeshSizeCm { get; set; }
    public double? MaxLengthMeters { get; set; }
    public string BanReason { get; set; } = string.Empty;
}

public class UpdateRestrictedToolDto : CreateRestrictedToolDto { }

// 3. Fishing Seasons
public class CreateFishingSeasonDto
{
    public string SeasonName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Region { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public List<string> RestrictedFishSpecies { get; set; } = new();
    public List<string> BannedTools { get; set; } = new();
    public bool IsStrictlyEnforced { get; set; }
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
