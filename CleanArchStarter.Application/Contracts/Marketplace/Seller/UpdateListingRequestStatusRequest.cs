using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Seller
{
    public class UpdateListingRequestStatusRequest
    {
        public Guid RequestId { get; init; }
        public bool IsApproved { get; init; }
        public string? RejectionReason { get; init; }
    }

}
