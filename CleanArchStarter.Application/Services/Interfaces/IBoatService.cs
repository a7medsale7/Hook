using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Boat;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces;

public interface IBoatService
{
    Task<Result<BoatResponse>> CreateAsync(string userId, CreateBoatRequest request, CancellationToken cancellationToken = default);
    Task<Result<BoatResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BoatResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<BoatResponse>>> GetMyBoatsAsync(string userId, CancellationToken cancellationToken = default);
    Task<Result<BoatResponse>> UpdateAsync(Guid id, string userId, UpdateBoatRequest request, bool isAdmin = false, CancellationToken cancellationToken = default);
    Task<Result> SoftDeleteAsync(Guid id, string userId, bool isAdmin = false, CancellationToken cancellationToken = default);
    Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
}
