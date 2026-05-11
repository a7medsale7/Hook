using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Admin
{
    public record AdminMarketplaceProductResponse
    (
    Guid Id,
    string Title,
    decimal Price,
    MarketplaceProductCondition Condition,
    MarketplaceProductCategory Category,
    int StockQuantity,
    bool IsActive,
    Guid SellerProfileId,
    string SellerName,
    string? MainImageUrl,
    DateTime CreatedOn
    );

}
