using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class CommunityHomeService(ApplicationDbContext context) : ICommunityHomeService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<List<HomeItemResponse>>> GetHomeBoatsAsync(CancellationToken cancellationToken = default)
    {
        var boats = await _context.Boats
            .Where(b => !b.IsDeleted)
            .Select(b => new HomeItemResponse
            {
                Id = b.Id.ToString(),
                Name = b.Name,
                ImageUrl = b.Images.OrderBy(i => i.IsMainImage ? 0 : 1).Select(i => i.ImageUrl).FirstOrDefault(),
                Title = "Boat"
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(boats);
    }

    public async Task<Result<List<HomeItemResponse>>> GetHomeBoatOwnersAsync(CancellationToken cancellationToken = default)
    {
        var boatOwners = await _context.BoatOwnerProfiles
            .Where(p => p.Status == RequestStatus.Approved && !p.IsDeleted)
            .Select(p => new HomeItemResponse
            {
                Id = p.User.Id,
                Name = p.User.FirstName + " " + p.User.LastName,
                ImageUrl = p.User.ProfilePictureUrl,
                Title = "Boat Owner"
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(boatOwners);
    }

    public async Task<Result<List<HomeItemResponse>>> GetHomeSellersAsync(CancellationToken cancellationToken = default)
    {
        var sellers = await _context.SellerProfiles
            .Where(s => s.Status == RequestStatus.Approved && !s.IsDeleted)
            .Select(s => new HomeItemResponse
            {
                Id = s.User.Id,
                Name = s.SellerName,
                ImageUrl = s.StoreImageUrl ?? s.User.ProfilePictureUrl,
                Title = "Seller"
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(sellers);
    }

    public async Task<Result<List<HomeItemResponse>>> GetHomeProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.MarketplaceProducts
            .Where(p => p.IsActive && !p.IsDeleted)
            .Select(p => new HomeItemResponse
            {
                Id = p.Id.ToString(),
                Name = p.Title,
                ImageUrl = p.Images.OrderBy(i => i.IsMainImage ? 0 : 1).Select(i => i.ImageUrl).FirstOrDefault(),
                Title = "Product"
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(products);
    }
}
