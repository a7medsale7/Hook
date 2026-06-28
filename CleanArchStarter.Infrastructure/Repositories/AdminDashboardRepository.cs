using Hook.Domain.Abstractions.Repositories;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Infrastructure.Repositories;

public class AdminDashboardRepository(ApplicationDbContext context) : IAdminDashboardRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> GetTotalUsersAsync() => await _context.Users.CountAsync();
    
    public async Task<int> GetTotalTripManagersAsync() => await _context.BoatOwnerProfiles.CountAsync();
    
    public async Task<int> GetTotalSellersAsync() => await _context.SellerProfiles.CountAsync();
    
    public async Task<int> GetTotalTripsAsync() => await _context.Trips.CountAsync();
    
    public async Task<int> GetTotalProductsAsync() => await _context.MarketplaceProducts.CountAsync();
    
    public async Task<int> GetTotalOrdersAsync() => await _context.MarketplaceOrders.CountAsync();
    
    public async Task<int> GetTotalBookingsAsync() => await _context.Bookings.CountAsync();
    
    public async Task<decimal> GetTotalRevenueAsync() 
    {
        return await _context.Payments
            .Where(p => p.Status == Hook.Domain.Enums.PaymentStatus.Completed)
            .SumAsync(p => p.Amount);
    }
}
