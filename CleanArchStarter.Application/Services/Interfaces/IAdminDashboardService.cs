using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Admin;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IAdminDashboardService
{
    Task<Result<AdminDashboardStatsResponse>> GetDashboardStatsAsync(CancellationToken cancellationToken = default);
}
