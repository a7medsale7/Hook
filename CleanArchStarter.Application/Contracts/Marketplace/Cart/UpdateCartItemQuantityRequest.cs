using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Cart
{
    public class UpdateCartItemQuantityRequest
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
    }

}
