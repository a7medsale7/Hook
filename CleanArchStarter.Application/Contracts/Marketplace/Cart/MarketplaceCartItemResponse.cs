using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Cart
{
    public record MarketplaceCartItemResponse(
     Guid ProductId,
     string Title,
     decimal Price,
     int Quantity,
     int StockQuantity,
     MarketplaceProductCondition Condition,
     MarketplaceProductCategory Category,
     string? MainImageUrl,
     Guid SellerProfileId,
     string SellerName
    );

}
