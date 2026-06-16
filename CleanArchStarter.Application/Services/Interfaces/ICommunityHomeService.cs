using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community.Home;

namespace Hook.Application.Services.Interfaces;

public interface ICommunityHomeService
{
    Task<Result<List<HomeBoatResponse>>> GetHomeBoatsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeBoatOwnerResponse>>> GetHomeBoatOwnersAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeSellerResponse>>> GetHomeSellersAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeProductResponse>>> GetHomeProductsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomeTripResponse>>> GetHomeTripsAsync(CancellationToken cancellationToken = default);
    Task<Result<List<HomePostResponse>>> GetHomePostsAsync(CancellationToken cancellationToken = default);
}
