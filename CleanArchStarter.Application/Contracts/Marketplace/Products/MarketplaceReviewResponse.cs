using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Products
{
    public record MarketplaceReviewResponse(
    Guid Id,
    int Rating,
    string? Comment,
    string BuyerName,
    string? BuyerImageUrl,
    DateTime CreatedOn
    );

}
