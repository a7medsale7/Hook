using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Enums
{
    public enum MarketplaceOrderStatus
    {
        Pending = 1,
        OutForDelivery = 2,
        DeliveredConfirmedByBuyer = 3,
        Cancelled = 4 
    }
}
