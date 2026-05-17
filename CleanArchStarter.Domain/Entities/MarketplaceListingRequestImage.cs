using Hook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceListingRequestImage : Auditable
    {
        public Guid Id { get; set; }

        public Guid ListingRequestId { get; set; }
        public virtual MarketplaceListingRequest ListingRequest { get; set; } = null!;

        public string ImageUrl { get; set; }= string.Empty;
        public bool IsMainImage { get; set; }
    }
}
