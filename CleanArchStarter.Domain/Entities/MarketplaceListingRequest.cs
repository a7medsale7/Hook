using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceListingRequest : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!;

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public string? AdminRejectionReason { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public MarketplaceProductCondition Condition { get; set; }
        public MarketplaceProductCategory Category { get; set; }
        public int StockQuantity { get; set; }

        public virtual ICollection<MarketplaceListingRequestImage> Images { get; set; } = new HashSet<MarketplaceListingRequestImage>();
    }
}
