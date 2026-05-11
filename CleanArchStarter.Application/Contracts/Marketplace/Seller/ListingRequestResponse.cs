using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Seller
{
    public record ListingRequestResponse(
     Guid Id,
     RequestStatus Status,
     string? AdminRejectionReason,
     string Title,
     decimal Price,
     MarketplaceProductCondition Condition,
     MarketplaceProductCategory Category,
     int StockQuantity,
     IReadOnlyList<string> ImageUrls,
     DateTime CreatedOn
 );
}
