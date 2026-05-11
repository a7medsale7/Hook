using Hook.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Seller
{
    public class CreateListingRequest
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public MarketplaceProductCondition Condition { get; init; }
        public MarketplaceProductCategory Category { get; init; }
        public int StockQuantity { get; init; }

        public IEnumerable<IFormFile>? Photos { get; init; }
        public int MainImageIndex { get; init; } = 0;
    }
}
