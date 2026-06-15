using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Services.Interfaces;

public interface ICommunityHomeService
{
    Task<Result<List<HomeItemResponse>>> GetHomeBoatsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeItemResponse>>> GetHomeBoatOwnersAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeItemResponse>>> GetHomeSellersAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeItemResponse>>> GetHomeProductsAsync(CancellationToken cancellationToken = default);
}
