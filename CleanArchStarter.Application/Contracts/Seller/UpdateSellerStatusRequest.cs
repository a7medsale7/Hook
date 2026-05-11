using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Seller
{
    public class UpdateSellerStatusRequest
    {
        public Guid ProfileId { get; set; }
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
    }

}
