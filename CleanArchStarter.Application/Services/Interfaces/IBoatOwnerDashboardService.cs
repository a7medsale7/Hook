using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.BoatOwner.Dashboard;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IBoatOwnerDashboardService
    {
        Task<Result<BoatOwnerStatisticsResponse>> GetStatisticsAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<UpcomingBookingResponse>>> GetUpcomingBookingsAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ActiveTripResponse>>> GetActiveTripsAsync(string userId, CancellationToken cancellationToken = default);
    }
}
