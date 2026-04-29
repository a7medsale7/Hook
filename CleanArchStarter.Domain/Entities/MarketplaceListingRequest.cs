using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceListingRequest
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; }


        public RequestStatus Status { get; set; } = RequestStatus.Pending; 
        public string? AdminRejectionReason { get; set; } 

        public string Title { get; set; }
        = string.Empty;

        public string Description { get; set; } 
        = string.Empty;

        public decimal price { get; set; }
        public MarketplaceProductCondition Condition { get; set; }
        public MarketplaceProductCategory Category { get; set; }
        public int StockQuantity { get; set; }

        public virtual MarketplaceListingRequestImage Images { get; set; } = new HashSet<MarketplaceListingRequestImage>();
    }
}
