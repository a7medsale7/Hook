using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class SellerProfile : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = null!;

        public string SellerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string NationalIdPhotoUrl { get; set; } = string.Empty;

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public string? AdminRejectionReason { get; set; }

        public virtual ICollection<MarketplaceProduct> Products { get; set; } = new HashSet<MarketplaceProduct>();
        public virtual ICollection<MarketplaceOrder> Orders { get; set; } = new HashSet<MarketplaceOrder>();


    }
}
