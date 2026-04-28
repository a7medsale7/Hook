using System;
using System.Collections.Generic;

namespace Hook.Application.Contracts.Boat;

public class BoatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public Guid OwnerProfileId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public List<BoatImageResponse> Images { get; set; } = new();
    public string? MainImageUrl { get; set; }
}

public class BoatImageResponse
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsMainImage { get; set; }
}