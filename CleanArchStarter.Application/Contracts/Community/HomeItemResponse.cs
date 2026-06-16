namespace Hook.Application.Contracts.Community;

public class HomeItemResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? OwnerName { get; set; }
    public string? OwnerContact { get; set; }
    public decimal? Price { get; set; }
    public System.DateTime? Date { get; set; }
}
