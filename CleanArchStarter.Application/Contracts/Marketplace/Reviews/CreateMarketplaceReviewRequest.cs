using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Contracts.Marketplace.Reviews
{
    public class CreateMarketplaceReviewRequest
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Rating { get; init; } // 1..5
        public string? Comment { get; init; }
    }

}
