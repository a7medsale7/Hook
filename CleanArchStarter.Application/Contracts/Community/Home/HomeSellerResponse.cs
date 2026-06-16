namespace Hook.Application.Contracts.Community.Home;

public class HomeSellerResponse
{
    public string Id { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string? StoreImageUrl { get; set; }
    public string ContactNumber { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
