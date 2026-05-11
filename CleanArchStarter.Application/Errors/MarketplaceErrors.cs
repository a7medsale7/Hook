using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceErrors
    {
        public static readonly Error ProductNotFound = new("Marketplace.ProductNotFound", "Product not found");
    }
}
