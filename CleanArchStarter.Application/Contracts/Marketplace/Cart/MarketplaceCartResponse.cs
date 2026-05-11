using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Cart
{
    public record MarketplaceCartResponse(
      IReadOnlyList<MarketplaceCartItemResponse> Items,
      decimal SubTotal
    );

}
