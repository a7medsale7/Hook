using Hook.Application.Abstractions.Result;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceOrderErrors
    {
        public static readonly Error InvalidQuantity = new("Marketplace.Order.InvalidQuantity","Quantity must be greater than zero");
        public static readonly Error NoItems = new("Marketplace.Order.NoItems", "Order must contain at least one item");
        public static readonly Error ProductNotFound = new("Marketplace.Order.ProductNotFound", "Product not found");
        public static readonly Error ProductInactive = new("Marketplace.Order.ProductInactive", "Product is not available");
        public static readonly Error SellerNotApproved = new("Marketplace.Order.SellerNotApproved", "Seller is not approved");
        public static readonly Error InsufficientStock = new("Marketplace.Order.InsufficientStock", "Requested quantity is not available in stock");
        public static readonly Error OrderNotFound = new("Marketplace.Order.NotFound", "Order not found");
        public static readonly Error Forbidden = new("Marketplace.Order.Forbidden", "You do not have permission to perform this action");
        public static readonly Error InvalidStatus = new("Marketplace.Order.InvalidStatus", "Invalid status transition");
        public static readonly Error CancellationReasonRequired = new("Marketplace.Order.CancellationReasonRequired", "Cancellation reason is required");
    }
}
