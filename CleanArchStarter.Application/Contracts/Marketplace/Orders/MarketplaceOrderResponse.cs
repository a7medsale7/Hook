using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Orders
{
    public record MarketplaceOrderResponse(
      Guid Id,
      MarketplaceOrderStatus Status,
      MarketplacePaymentMethod PaymentMethod,
      decimal SubTotal,
      decimal Total,
      DateTime CreatedOn,
      string SellerName,
      Guid SellerProfileId,
      IReadOnlyList<MarketplaceOrderItemResponse> Items,
      string ContactEmail,
      string ContactPhone,
      string Governorate,
      string City,
      string FirstName,
      string LastName,
      string Address,
      string? PostalCode,
      string? CancellationReason
  );
}
