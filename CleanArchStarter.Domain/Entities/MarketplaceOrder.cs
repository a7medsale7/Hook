using Hook.Domain.Entities.Base;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Domain.Entities
{
    public class MarketplaceOrder : Auditable
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BuyerUserId { get; set; } = string.Empty;
        public virtual ApplicationUser BuyerUser { get; set; } = null!;

        public Guid SellerProfileId { get; set; }
        public virtual SellerProfile SellerProfile { get; set; }= null!;

        public MarketplaceOrderStatus Status { get; set; } = MarketplaceOrderStatus.Pending;
        public MarketplacePaymentMethod PaymentMethod { get; set; }

        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;

        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? PostalCode { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string? CancellationReason { get; set; }

        public virtual ICollection<MarketplaceOrderItem> Items { get; set; } = new HashSet<MarketplaceOrderItem>();

    }
}
