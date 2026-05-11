using Hangfire.States;
using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceCartErrors
    {
        public static readonly Error InvalidQuantity = new("Marketplace.Cart.InvalidQuantity","Quantity must be greater than zero");
        public static readonly Error ProductNotFound = new("Marketplace.Cart.ProductNotFound", "Product not found");
        public static readonly Error ProductInactive = new("Marketplace.Cart.ProductInactive", "Product is not available");
        public static readonly Error InsufficientStock = new("Marketplace.Cart.InsufficientStock", "Requested quantity is not available in stock");
    }
}
