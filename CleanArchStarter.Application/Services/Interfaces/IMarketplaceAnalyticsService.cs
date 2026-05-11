using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceAnalyticsService
    {
        Task<Result<SellerMarketplaceStatsResponse>> GetSellerStatsAsync(string sellerUserId, CancellationToken cancellationToken = default);
        Task<Result<AdminMarketplaceStatsResponse>> GetAdminStatsAsync(CancellationToken cancellationToken = default);
    }
}
