using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Admin;
using Hook.Application.Contracts.Seller;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Consts;
using Hook.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceAdminManagementService : IMarketplaceAdminManagementService
    {
        private readonly ISellerProfileRepository _sellerProfileRepository;
        private readonly IMarketplaceProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public MarketplaceAdminManagementService(
            ISellerProfileRepository sellerProfileRepository,
            IMarketplaceProductRepository productRepository,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IFileService fileService)
        {
            _sellerProfileRepository = sellerProfileRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Result<IEnumerable<SellerResponse>>> GetAllSellersAsync(CancellationToken cancellationToken = default)
        {
            var sellers = await _sellerProfileRepository.GetAllAsync();
            return Result.Success(sellers.Select(ToResponse));
        }

        public async Task<Result> DeleteSellerAsync(Guid sellerProfileId, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByIdAsync(sellerProfileId);
            if (profile is null)
                return Result.Failure(SellerErrors.ProfileNotFound);

            _sellerProfileRepository.SoftDelete(profile);

            var user = profile.User;
            if (user is not null)
            {
                await _userManager.RemoveFromRoleAsync(user, DefaultRoles.Seller);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<AdminMarketplaceProductResponse>>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllForAdminAsync();
            var response = products.Select(ToAdminProductResponse);
            return Result.Success(response);
        }

        public async Task<Result> DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null)
                return Result.Failure(MarketplaceSellerProductErrors.ProductNotFound);

            foreach (var img in product.Images)
            {
                _fileService.DeleteFile(img.ImageUrl);
            }

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private static SellerResponse ToResponse(SellerProfile profile) => new SellerResponse
        {
            Id = profile.Id,
            UserId = profile.UserId,
            FullName = profile.User is not null ? $"{profile.User.FirstName} {profile.User.LastName}" : "Unknown",
            Email = profile.User?.Email ?? string.Empty,
            SellerName = profile.SellerName,
            PhoneNumber = profile.PhoneNumber,
            Governorate = profile.Governorate,
            City = profile.City,
            Address = profile.Address,
            NationalIdPhotoUrl = profile.NationalIdPhotoUrl,
            Status = profile.Status,
            AdminRejectionReason = profile.AdminRejectionReason,
            CreatedOn = profile.CreatedOn
        };

        private static AdminMarketplaceProductResponse ToAdminProductResponse(MarketplaceProduct p)
        {
            var main = p.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl;
            var sellerName = p.SellerProfile is null
                ? "Unknown"
                : p.SellerProfile.SellerName;

            return new AdminMarketplaceProductResponse(
                p.Id,
                p.Title,
                p.Price,
                p.Condition,
                p.Category,
                p.StockQuantity,
                p.IsActive,
                p.SellerProfileId,
                sellerName,
                main,
                p.CreatedOn
            );
        }
    }
}
    
