using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Admin;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class AdminDashboardService(IAdminDashboardRepository adminDashboardRepository) : IAdminDashboardService
{
    private readonly IAdminDashboardRepository _adminDashboardRepository = adminDashboardRepository;

    public async Task<Result<AdminDashboardStatsResponse>> GetDashboardStatsAsync(CancellationToken cancellationToken = default)
    {
        var usersCount = await _adminDashboardRepository.GetTotalUsersAsync();
        var tripManagersCount = await _adminDashboardRepository.GetTotalTripManagersAsync();
        var sellersCount = await _adminDashboardRepository.GetTotalSellersAsync();
        var tripsCount = await _adminDashboardRepository.GetTotalTripsAsync();
        var productsCount = await _adminDashboardRepository.GetTotalProductsAsync();
        var ordersCount = await _adminDashboardRepository.GetTotalOrdersAsync();
        var bookingsCount = await _adminDashboardRepository.GetTotalBookingsAsync();
        var revenue = await _adminDashboardRepository.GetTotalRevenueAsync();

        var response = new AdminDashboardStatsResponse(
            usersCount,
            tripManagersCount,
            sellersCount,
            tripsCount,
            productsCount,
            ordersCount,
            revenue,
            bookingsCount
        );

        return Result.Success(response);
    }
}
