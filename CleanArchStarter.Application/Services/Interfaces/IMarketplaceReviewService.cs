using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceReviewService
    {
        Task<Result<MarketplaceReviewPublicResponse>> CreateAsync(string buyerUserId, CreateMarketplaceReviewRequest request, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<MarketplaceReviewPublicResponse>>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default);
    }
}
