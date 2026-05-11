using Hook.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Products
{
    public class UpdateMarketplaceProductRequest
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public MarketplaceProductCondition Condition { get; set; }
        public MarketplaceProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Optional: replace images if provided
        public List<IFormFile>? NewImages { get; set; }
    }

}
