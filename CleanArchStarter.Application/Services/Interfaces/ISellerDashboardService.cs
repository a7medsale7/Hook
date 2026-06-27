using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.SellerDashboard;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface ISellerDashboardService
    {
        Task<Result<SellerDashboardStatsResponse>> GetStatisticsAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerRecentOrderResponse>>> GetRecentOrdersAsync(string userId, int count = 20, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerRecentReviewResponse>>> GetRecentReviewsAsync(string userId, int count = 20, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerMonthlySalesResponse>>> GetSalesOverTimeAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerTopProductResponse>>> GetTopSellingProductsAsync(string userId, int count = 5, CancellationToken cancellationToken = default);
    }
}
