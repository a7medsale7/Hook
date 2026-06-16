namespace Hook.Application.Contracts.Community.Home;

public class HomeBoatResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerContact { get; set; } = string.Empty;
}
