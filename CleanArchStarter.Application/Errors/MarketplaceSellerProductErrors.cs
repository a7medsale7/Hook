using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceSellerProductErrors
    {
        public static readonly Error NotApprovedSeller =
        new("Marketplace.SellerNotApproved", "Your seller account is not approved");

        public static readonly Error ProductNotFound =
            new("Marketplace.ProductNotFound", "Product not found");

        public static readonly Error Forbidden =
            new("Marketplace.Forbidden", "You are not allowed to access this resource");
    }
}
