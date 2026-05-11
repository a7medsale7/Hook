using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Orders
{
    public record MarketplaceOrderItemResponse(
        Guid ProductId,
        string Title,
        decimal UnitPrice,
        int Quantity,
        decimal LineTotal,
        MarketplaceProductCategory Category,
        MarketplaceProductCondition Condition,
        string? MainImageUrl
    );

}
