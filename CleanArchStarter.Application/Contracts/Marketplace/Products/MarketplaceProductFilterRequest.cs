using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Products
{
    public class MarketplaceProductFilterRequest : ReqeustFilters
    {
        public MarketplaceProductCategory? Category { get; init; }
        public MarketplaceProductCondition? Condition { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public string? Query { get; init; }
    }

}
