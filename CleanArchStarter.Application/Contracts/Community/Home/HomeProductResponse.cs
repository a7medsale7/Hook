namespace Hook.Application.Contracts.Community.Home;

public class HomeProductResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string SellerName { get; set; } = string.Empty;
    public string SellerContact { get; set; } = string.Empty;
    public string? SellerStoreImageUrl { get; set; }
}
