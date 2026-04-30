using Hook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceReview : Auditable
    {
        public Guid Id {  get; set; } =  Guid.NewGuid();

        public Guid ProductId { get; set; }
        public virtual MarketplaceProduct Product { get; set; } = null!;

        public Guid OrderId {  get; set; }
        public virtual MarketplaceOrder Order { get; set; } = null!;

        public string BuyerUserId { get; set; } = string.Empty;
        public virtual ApplicationUser Buyer { get; set; }

        public int Rating { get; set; } // 1..5
        public string? Comment { get; set; }

    }
}
