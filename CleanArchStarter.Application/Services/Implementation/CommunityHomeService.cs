using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Community.Home;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Enums;
using Hook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class CommunityHomeService(ApplicationDbContext context) : ICommunityHomeService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<List<HomeBoatResponse>>> GetHomeBoatsAsync(CancellationToken cancellationToken = default)
    {
        var boats = await _context.Boats
            .Where(b => !b.IsDeleted)
            .OrderBy(b => Guid.NewGuid())
            .Select(b => new HomeBoatResponse
            {
                Id = b.Id.ToString(),
                Name = b.Name,
                ImageUrl = b.Images.OrderBy(i => i.IsMainImage ? 0 : 1).Select(i => i.ImageUrl).FirstOrDefault(),
                Description = b.Description,
                Capacity = b.Capacity,
                OwnerName = b.OwnerProfile.User.FirstName + " " + b.OwnerProfile.User.LastName,
                OwnerContact = b.OwnerProfile.User.PhoneNumber
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(boats);
    }

    public async Task<Result<List<HomeBoatOwnerResponse>>> GetHomeBoatOwnersAsync(CancellationToken cancellationToken = default)
    {
        var boatOwners = await _context.BoatOwnerProfiles
            .Where(p => p.Status == RequestStatus.Approved && !p.IsDeleted)
            .OrderBy(p => Guid.NewGuid())
            .Select(p => new HomeBoatOwnerResponse
            {
                Id = p.User.Id,
                Name = p.User.FirstName + " " + p.User.LastName,
                ImageUrl = p.User.ProfilePictureUrl,
                ContactNumber = p.User.PhoneNumber
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(boatOwners);
    }

    public async Task<Result<List<HomeSellerResponse>>> GetHomeSellersAsync(CancellationToken cancellationToken = default)
    {
        var sellers = await _context.SellerProfiles
            .Where(s => s.Status == RequestStatus.Approved && !s.IsDeleted)
            .OrderBy(s => Guid.NewGuid())
            .Select(s => new HomeSellerResponse
            {
                Id = s.User.Id,
                SellerName = s.SellerName,
                StoreImageUrl = s.StoreImageUrl ?? s.User.ProfilePictureUrl,
                Location = s.Governorate + ", " + s.City,
                ContactNumber = s.User.PhoneNumber
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(sellers);
    }

    public async Task<Result<List<HomeProductResponse>>> GetHomeProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.MarketplaceProducts
            .Where(p => p.IsActive && !p.IsDeleted)
            .OrderBy(p => Guid.NewGuid())
            .Select(p => new HomeProductResponse
            {
                Id = p.Id.ToString(),
                Title = p.Title,
                ImageUrl = p.Images.OrderBy(i => i.IsMainImage ? 0 : 1).Select(i => i.ImageUrl).FirstOrDefault(),
                Description = p.Description,
                Price = p.Price,
                SellerName = p.SellerProfile.SellerName,
                SellerStoreImageUrl = p.SellerProfile.StoreImageUrl,
                SellerContact = p.SellerProfile.User.PhoneNumber
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(products);
    }

    public async Task<Result<List<HomeTripResponse>>> GetHomeTripsAsync(CancellationToken cancellationToken = default)
    {
        var trips = await _context.Trips
            .OrderBy(t => Guid.NewGuid())
            .Select(t => new HomeTripResponse
            {
                Id = t.Id.ToString(),
                Title = t.Title,
                ImageUrl = t.Images.OrderBy(i => i.IsMainImage ? 0 : 1).Select(i => i.ImageUrl).FirstOrDefault(),
                Description = t.ShortDescription,
                Price = t.PricePerPerson,
                BoatName = t.Boat.Name,
                LocationName = t.LocationName,
                OwnerName = t.Boat.OwnerProfile.User.FirstName + " " + t.Boat.OwnerProfile.User.LastName,
                OwnerImageUrl = t.Boat.OwnerProfile.User.ProfilePictureUrl,
                OwnerContact = t.Boat.OwnerProfile.User.PhoneNumber
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(trips);
    }

    public async Task<Result<List<HomePostResponse>>> GetHomePostsAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Images)
            .OrderBy(p => Guid.NewGuid())
            .Select(p => new HomePostResponse
            {
                Id = p.Id.ToString(),
                OwnerId = p.User.Id,
                OwnerName = p.User.FirstName + " " + p.User.LastName,
                OwnerImageUrl = p.User.ProfilePictureUrl,
                Content = p.Content,
                PostImageUrl = p.Images.Select(i => i.ImageUrl).FirstOrDefault(),
                LikesCount = p.Likes.Count,
                CommentsCount = p.Comments.Count,
                Date = p.CreatedOn
            })
            .Take(5)
            .ToListAsync(cancellationToken);

        return Result.Success(posts);
    }
}
