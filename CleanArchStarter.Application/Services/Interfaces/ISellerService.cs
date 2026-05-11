using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface ISellerService
    {
        Task<Result<SellerResponse>> ApplyAsync(string userId, ApplySellerRequest request, CancellationToken cancellationToken = default);
        Task<Result<SellerResponse>> GetProfileAsync(string userId, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerResponse>>> GetPendingApplicationsAsync(CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result> UpdateStatusAsync(UpdateSellerStatusRequest request, CancellationToken cancellationToken = default);
        Task<Result> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<SellerResponse>>> GetDeletedAsync(CancellationToken cancellationToken = default);
        Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
