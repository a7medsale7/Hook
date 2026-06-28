using System.Threading.Tasks;

namespace Hook.Domain.Abstractions.Repositories;

public interface IAdminDashboardRepository
{
    Task<int> GetTotalUsersAsync();
    Task<int> GetTotalTripManagersAsync();
    Task<int> GetTotalSellersAsync();
    Task<int> GetTotalTripsAsync();
    Task<int> GetTotalProductsAsync();
    Task<int> GetTotalOrdersAsync();
    Task<decimal> GetTotalRevenueAsync();
    Task<int> GetTotalBookingsAsync();
}
