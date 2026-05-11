using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Products
{
    public record MarketplaceProductDetailsResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price,
    MarketplaceProductCondition Condition,
    MarketplaceProductCategory Category,
    int StockQuantity,
    string SellerName,
    Guid SellerProfileId,
    IReadOnlyList<string> ImageUrls,
    IReadOnlyList<MarketplaceReviewResponse> Reviews,
    decimal AverageRating,
    int ReviewsCount
    );

}
