using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Orders
{
    public class CreateMarketplaceOrderRequest
    {
        public List<MarketplaceOrderItemRequest> Items { get; init; } = new();

        public string ContactEmail { get; init; } = string.Empty;
        public string ContactPhone { get; init; } = string.Empty;

        public string Governorate { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Address { get; init; } = string.Empty;
        public string? PostalCode { get; init; }

        public MarketplacePaymentMethod PaymentMethod { get; init; }

        // If true, will clear purchased items from cart
        public bool ClearCartItems { get; init; } = false;
    }

}
