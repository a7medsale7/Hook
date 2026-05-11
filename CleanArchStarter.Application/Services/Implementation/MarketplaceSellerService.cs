using Hangfire;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Seller;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Consts;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceSellerService(
     IMarketplaceListingRequestRepository listingRequestRepository,
     ISellerProfileRepository sellerProfileRepository,
     IMarketplaceProductRepository productRepository,
     IFileService fileService,
     UserManager<ApplicationUser> userManager,
     IBackgroundJobClient backgroundJobClient,
     IUnitOfWork unitOfWork) : IMarketplaceSellerService
    {
        public async Task<Result<ListingRequestResponse>> CreateListingRequestAsync(string userId, CreateListingRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || request.Price <= 0 || request.StockQuantity < 0)
                return Result.Failure<ListingRequestResponse>(MarketplaceSellerErrors.InvalidData);

            // allow multiple listing requests, but if already has approved seller profile, still allow listing requests (admin approval workflow)
            var images = await fileService.SaveFilesAsync(request.Photos, "uploads/marketplace/listing-requests");
            var reqImages = images.Select((url, idx) => new MarketplaceListingRequestImage
            {
                ImageUrl = url,
                IsMainImage = idx == request.MainImageIndex
            }).ToList();

            if (reqImages.Any() && !reqImages.Any(i => i.IsMainImage))
                reqImages[0].IsMainImage = true;

            var listing = new MarketplaceListingRequest
            {
                UserId = userId,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Condition = request.Condition,
                Category = request.Category,
                StockQuantity = request.StockQuantity,
                Status = RequestStatus.Pending,
                Images = reqImages
            };

            await listingRequestRepository.AddAsync(listing);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(ToResponse(listing));
        }

        public async Task<Result<IEnumerable<ListingRequestResponse>>> GetMyListingRequestsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var reqs = await listingRequestRepository.GetByUserIdAsync(userId);
            return Result.Success(reqs.Select(ToResponse));
        }

        public async Task<Result<IEnumerable<ListingRequestResponse>>> GetPendingListingRequestsAsync(CancellationToken cancellationToken = default)
        {
            var pending = await listingRequestRepository.GetPendingAsync();
            return Result.Success(pending.Select(ToResponse));
        }

        public async Task<Result> UpdateListingRequestStatusAsync(UpdateListingRequestStatusRequest request, CancellationToken cancellationToken = default)
        {
            var listing = await listingRequestRepository.GetByIdWithDetailsAsync(request.RequestId);
            if (listing is null)
                return Result.Failure(MarketplaceSellerErrors.RequestNotFound);

            if (request.IsApproved)
            {
                listing.Status = RequestStatus.Approved;
                listing.AdminRejectionReason = null;

                // Ensure seller profile exists and approved + assign role Seller
                var sellerProfile = await sellerProfileRepository.GetByUserIdAsync(listing.UserId);
                if (sellerProfile is null)
                {
                    sellerProfile = new SellerProfile
                    {
                        UserId = listing.UserId,
                        Status = RequestStatus.Approved
                    };
                    await sellerProfileRepository.AddAsync(sellerProfile);
                }
                else
                {
                    sellerProfile.Status = RequestStatus.Approved;
                    sellerProfileRepository.Update(sellerProfile);
                }

                var user = await userManager.FindByIdAsync(listing.UserId);
                if (user != null)
                {
                    await userManager.AddToRoleAsync(user, DefaultRoles.Seller);
                }

                // Create product from listing
                var product = new MarketplaceProduct
                {
                    SellerProfileId = sellerProfile.Id,
                    Title = listing.Title,
                    Description = listing.Description,
                    Price = listing.Price,
                    Condition = listing.Condition,
                    Category = listing.Category,
                    StockQuantity = listing.StockQuantity,
                    IsActive = true,
                    Images = listing.Images.Select(i => new MarketplaceProductImage
                    {
                        ImageUrl = i.ImageUrl,
                        IsMainImage = i.IsMainImage
                    }).ToList()
                };
                await productRepository.AddAsync(product);

                // Email user approved
                try
                {
                    if (user?.Email != null)
                    {
                        var html = EmailTemplates.GetMarketplaceListingApprovedTemplate(user.FirstName, listing.Title);
                        backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(user.Email, "✅ Listing approved", html));
                    }
                }
                catch { }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.RejectionReason))
                    return Result.Failure(MarketplaceSellerErrors.RejectionReasonRequired);

                listing.Status = RequestStatus.Rejected;
                listing.AdminRejectionReason = request.RejectionReason.Trim();

                // Email user rejected
                try
                {
                    var user = await userManager.FindByIdAsync(listing.UserId);
                    if (user?.Email != null)
                    {
                        var html = EmailTemplates.GetMarketplaceListingRejectedTemplate(user.FirstName, listing.Title, listing.AdminRejectionReason);
                        backgroundJobClient.Enqueue<IEmailSender>(s => s.SendEmailAsync(user.Email, "❌ Listing rejected", html));
                    }
                }
                catch { }
            }

            listingRequestRepository.Update(listing);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private static ListingRequestResponse ToResponse(MarketplaceListingRequest r) => new(
            r.Id,
            r.Status,
            r.AdminRejectionReason,
            r.Title,
            r.Price,
            r.Condition,
            r.Category,
            r.StockQuantity,
            r.Images.OrderByDescending(i => i.IsMainImage).Select(i => i.ImageUrl).ToList(),
            r.CreatedOn
        );
    }


}
