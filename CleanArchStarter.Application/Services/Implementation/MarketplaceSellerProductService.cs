using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Marketplace.Products;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class MarketplaceSellerProductService : IMarketplaceSellerProductService
    {
        private readonly ISellerProfileRepository _sellerProfileRepository;
        private readonly IMarketplaceProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public MarketplaceSellerProductService(
            ISellerProfileRepository sellerProfileRepository,
            IMarketplaceProductRepository productRepository,
            IFileService fileService,
            IUnitOfWork unitOfWork)
        {
            _sellerProfileRepository = sellerProfileRepository;
            _productRepository = productRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> CreateAsync(string userId, CreateMarketplaceProductRequest request, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<Guid>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var product = new MarketplaceProduct
            {
                SellerProfileId = sellerProfile.Id,
                Title = request.Title,
                Description = request.Description,
                Condition = request.Condition,
                Category = request.Category,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsActive = true
            };

            var images = request.Images ?? new List<Microsoft.AspNetCore.Http.IFormFile>();
            for (var i = 0; i < images.Count; i++)
            {
                var url = await _fileService.SaveFileAsync(images[i], "marketplace/products");
                product.Images.Add(new MarketplaceProductImage
                {
                    ImageUrl = url,
                    IsMainImage = i == 0
                });
            }

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(product.Id);
        }

        public async Task<Result> UpdateAsync(string userId, UpdateMarketplaceProductRequest request, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure(MarketplaceSellerProductErrors.NotApprovedSeller);

            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure(MarketplaceSellerProductErrors.ProductNotFound);

            if (product.SellerProfileId != sellerProfile.Id)
                return Result.Failure(MarketplaceSellerProductErrors.Forbidden);

            product.Title = request.Title;
            product.Description = request.Description;
            product.Condition = request.Condition;
            product.Category = request.Category;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;

            if (request.NewImages is not null && request.NewImages.Count > 0)
            {
                foreach (var img in product.Images.ToList())
                {
                    _fileService.DeleteFile(img.ImageUrl);
                    img.IsDeleted = true;
                }

                product.Images.Clear();

                for (var i = 0; i < request.NewImages.Count; i++)
                {
                    var url = await _fileService.SaveFileAsync(request.NewImages[i], "marketplace/products");
                    product.Images.Add(new MarketplaceProductImage
                    {
                        ImageUrl = url,
                        IsMainImage = i == 0
                    });
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(string userId, Guid productId, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure(MarketplaceSellerProductErrors.NotApprovedSeller);

            var product = await _productRepository.GetByIdAsync(productId);
            if (product is null)
                return Result.Failure(MarketplaceSellerProductErrors.ProductNotFound);

            if (product.SellerProfileId != sellerProfile.Id)
                return Result.Failure(MarketplaceSellerProductErrors.Forbidden);

            foreach (var img in product.Images)
            {
                _fileService.DeleteFile(img.ImageUrl);
            }

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<MarketplaceProductListItemResponse>>> GetMyProductsAsync(string userId, CancellationToken cancellationToken = default)
        {
            var sellerProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (sellerProfile is null || sellerProfile.Status != RequestStatus.Approved)
                return Result.Failure<IEnumerable<MarketplaceProductListItemResponse>>(MarketplaceSellerProductErrors.NotApprovedSeller);

            var products = await _productRepository.GetBySellerProfileIdAsync(sellerProfile.Id);
            var response = products.Select(ToListItem);
            return Result.Success(response);
        }

        private static MarketplaceProductListItemResponse ToListItem(MarketplaceProduct p)
        {
            var main = p.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? p.Images.FirstOrDefault()?.ImageUrl;
            return new MarketplaceProductListItemResponse(
                p.Id,
                p.Title,
                p.Price,
                p.Condition,
                p.Category,
                p.StockQuantity,
                main);
        }
    }


}
