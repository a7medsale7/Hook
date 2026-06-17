using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Hook.Domain.Entities.Base;

namespace Hook.Domain.Entities;

public class RestrictedTool : Auditable
{
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("material")]
    public string Material { get; set; } = string.Empty;

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public double? MinMeshSizeCm { get; set; }

    [JsonIgnore]
    public double? MaxLengthMeters { get; set; }

    [JsonIgnore]
    public string BanReason { get; set; } = string.Empty;

    [NotMapped]
    [JsonPropertyName("regulations")]
    public ToolRegulations Regulations
    {
        get => new ToolRegulations
        {
            MinMeshSizeCm = MinMeshSizeCm,
            MaxLengthMeters = MaxLengthMeters,
            BanReason = BanReason
        };
        set
        {
            if (value != null)
            {
                MinMeshSizeCm = value.MinMeshSizeCm;
                MaxLengthMeters = value.MaxLengthMeters;
                BanReason = value.BanReason;
            }
        }
    }
}

public class ToolRegulations
{
    [JsonPropertyName("min_mesh_size_cm")]
    public double? MinMeshSizeCm { get; set; }

    [JsonPropertyName("max_length_meters")]
    public double? MaxLengthMeters { get; set; }

    [JsonPropertyName("ban_reason")]
    public string BanReason { get; set; } = string.Empty;
}
