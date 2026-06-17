using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class FishingSeason : Auditable
{
    public int Id { get; set; }

    [JsonPropertyName("season_name")]
    public string SeasonName { get; set; } = string.Empty;

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = string.Empty;

    [JsonPropertyName("restricted_fish_species")]
    public List<string> RestrictedFishSpecies { get; set; } = new();

    [JsonPropertyName("banned_tools")]
    public List<string> BannedTools { get; set; } = new();

    [JsonPropertyName("is_strictly_enforced")]
    public bool IsStrictlyEnforced { get; set; }
}
