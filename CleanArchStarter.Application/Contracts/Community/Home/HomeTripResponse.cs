namespace Hook.Application.Contracts.Community.Home;

public class HomeTripResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string BoatName { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerContact { get; set; } = string.Empty;
    public string? OwnerImageUrl { get; set; }
}
