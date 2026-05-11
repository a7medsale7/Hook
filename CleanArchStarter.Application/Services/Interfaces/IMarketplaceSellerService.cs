using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Interfaces
{
    public interface IMarketplaceSellerService
    {
        Task<Result<ListingRequestResponse>> CreateListingRequestAsync(string userId, CreateListingRequest request, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ListingRequestResponse>>> GetMyListingRequestsAsync(string userId, CancellationToken cancellationToken = default);

        // Admin
        Task<Result<IEnumerable<ListingRequestResponse>>> GetPendingListingRequestsAsync(CancellationToken cancellationToken = default);
        Task<Result> UpdateListingRequestStatusAsync(UpdateListingRequestStatusRequest request, CancellationToken cancellationToken = default);
    }
}
