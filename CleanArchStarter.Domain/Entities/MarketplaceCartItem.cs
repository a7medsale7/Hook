using Hook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceCartItem : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BuyerUserId { get; set; } = string.Empty;
        public virtual ApplicationUser Buyer { get; set; } = null!;

        public Guid ProductId { get; set; }
        public virtual MarketplaceProduct Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}