using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceProduct : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SellerProfileId { get; set; }
        public virtual SellerProfile SellerProfile { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public MarketplaceProductCondition Condition { get; set; }
        public MarketplaceProductCategory Category { get; set; }

        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MarketplaceProductImage> Images { get; set; } = new HashSet<MarketplaceProductImage>();
        public virtual ICollection<MarketplaceReview> Reviews { get; set; } = new HashSet<MarketplaceReview>();
    }
}
