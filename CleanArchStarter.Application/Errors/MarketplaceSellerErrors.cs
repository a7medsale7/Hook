using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class MarketplaceSellerErrors
    {
        public static readonly Error AlreadyApplied = new("Marketplace.Seller.AlreadyApplied", "You already have a pending/approved seller profile.");
        public static readonly Error InvalidData = new("Marketplace.Seller.InvalidData", "Invalid listing request data.");
        public static readonly Error RequestNotFound = new("Marketplace.Seller.RequestNotFound", "Listing request not found.");
        public static readonly Error RejectionReasonRequired = new("Marketplace.Seller.RejectionReasonRequired", "Rejection reason is required.");
    }
}
