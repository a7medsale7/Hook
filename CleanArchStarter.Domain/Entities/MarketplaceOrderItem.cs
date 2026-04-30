using Hook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceOrderItem : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OrderId { get; set; } 
        public virtual MarketplaceOrderItem Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        public virtual MarketplaceProduct Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

    }
}
