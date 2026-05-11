using Hook.Application.Abstractions.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Errors
{
    public class SellerErrors
    {
        public static readonly Error ProfileNotFound =
        new("Seller.NotFound", "The seller profile was not found");

        public static readonly Error AlreadyApplied =
            new("Seller.AlreadyApplied", "You have already submitted an application");

        public static readonly Error NotApproved =
            new("Seller.NotApproved", "Your seller account is not yet approved or has been rejected");

        public static readonly Error RejectionReasonRequired =
            new("Seller.RejectionReasonRequired", "A rejection reason must be provided by the admin");
    }
}