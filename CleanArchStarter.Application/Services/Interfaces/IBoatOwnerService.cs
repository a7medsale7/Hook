using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.BoatOwner;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IBoatOwnerService
{
    Task<Result<BoatOwnerResponse>> ApplyAsync(string userId, ApplyBoatOwnerRequest request, CancellationToken cancellationToken = default);
    Task<Result<BoatOwnerResponse>> GetProfileAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BoatOwnerResponse>>> GetPendingApplicationsAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BoatOwnerResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result> UpdateStatusAsync(UpdateBoatOwnerStatusRequest request, CancellationToken cancellationToken = default);
    Task<Result> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BoatOwnerResponse>>> GetDeletedAsync(CancellationToken cancellationToken = default);
    Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
}
