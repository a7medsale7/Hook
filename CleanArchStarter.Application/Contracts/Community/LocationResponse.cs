using System;

namespace Hook.Application.Contracts.Community;

public class LocationResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Governorate { get; set; } = string.Empty;
}
