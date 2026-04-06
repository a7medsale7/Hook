using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Boat;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class BoatService : IBoatService
{
    private readonly IBoatRepository _boatRepository;
    private readonly IBoatOwnerRepository _boatOwnerRepository;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;

    public BoatService(
        IBoatRepository boatRepository,
        IBoatOwnerRepository boatOwnerRepository,
        IFileService fileService,
        IUnitOfWork unitOfWork)
    {
        _boatRepository = boatRepository;
        _boatOwnerRepository = boatOwnerRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<BoatResponse>> CreateAsync(string userId, CreateBoatRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Check if user has an approved owner profile
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<BoatResponse>(BoatErrors.NoOwnerProfile);

        if (ownerProfile.Status != RequestStatus.Approved)
            return Result.Failure<BoatResponse>(BoatErrors.NotApproved);

        // 2. Upload Images (Multiple)
        var imageUrls = await _fileService.SaveFilesAsync(request.Images, "uploads/boats");

        // 3. Create Boat
        var boat = new Boat
        {
            Name = request.Name,
            Description = request.Description,
            Capacity = request.Capacity,
            OwnerProfileId = ownerProfile.Id,
            Images = imageUrls.Select(url => new BoatImage { ImageUrl = url }).ToList()
        };

        await _boatRepository.AddAsync(boat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(ToResponse(boat, ownerProfile));
    }

    public async Task<Result<BoatResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var boat = await _boatRepository.GetByIdWithDetailsAsync(id);
        if (boat is null)
            return Result.Failure<BoatResponse>(BoatErrors.NotFound);

        return Result.Success(ToResponse(boat));
    }

    public async Task<Result<IEnumerable<BoatResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var boats = await _boatRepository.GetAllAsync();
        return Result.Success(boats.Select(b => ToResponse(b)));
    }

    public async Task<Result<IEnumerable<BoatResponse>>> GetMyBoatsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<IEnumerable<BoatResponse>>(BoatErrors.NoOwnerProfile);

        var boats = await _boatRepository.GetByOwnerIdAsync(ownerProfile.Id);
        return Result.Success(boats.Select(b => ToResponse(b, ownerProfile)));
    }

    public async Task<Result<BoatResponse>> UpdateAsync(Guid id, string userId, UpdateBoatRequest request, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var boat = await _boatRepository.GetByIdWithDetailsAsync(id);
        if (boat is null)
            return Result.Failure<BoatResponse>(BoatErrors.NotFound);

        // Check ownership (bypass for Admin)
        if (!isAdmin && boat.OwnerProfile.UserId != userId)
            return Result.Failure<BoatResponse>(BoatErrors.Unauthorized);

        // 1. Update basic info
        boat.Name = request.Name;
        boat.Description = request.Description;
        boat.Capacity = request.Capacity;

        // 2. Hande Image Deletions
        if (request.ImageIdsToDelete != null && request.ImageIdsToDelete.Any())
        {
            var imagesToDelete = boat.Images.Where(img => request.ImageIdsToDelete.Contains(img.Id)).ToList();
            foreach (var img in imagesToDelete)
            {
                _fileService.DeleteFile(img.ImageUrl);
                boat.Images.Remove(img);
            }
        }

        // 3. Handle New Images
        if (request.NewImages != null && request.NewImages.Any())
        {
            var newUrls = await _fileService.SaveFilesAsync(request.NewImages, "uploads/boats");
            foreach (var url in newUrls)
            {
                boat.Images.Add(new BoatImage { ImageUrl = url });
            }
        }

        _boatRepository.Update(boat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(ToResponse(boat));
    }

    public async Task<Result> SoftDeleteAsync(Guid id, string userId, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var boat = await _boatRepository.GetByIdAsync(id);
        if (boat is null)
            return Result.Failure(BoatErrors.NotFound);

        // Ensure owner profile is loaded for ownership check if not already there
        var boatWithDetails = await _boatRepository.GetByIdWithDetailsAsync(id);
        if (!isAdmin && boatWithDetails?.OwnerProfile.UserId != userId)
            return Result.Failure(BoatErrors.Unauthorized);

        _boatRepository.Delete(boat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Typically only Admin or Owner can restore. Let's assume generic for now
        var boats = await _boatRepository.GetDeletedAsync();
        var boat = boats.FirstOrDefault(b => b.Id == id);
        
        if (boat is null)
            return Result.Failure(BoatErrors.NotFound);

        boat.IsDeleted = false;
        _boatRepository.Update(boat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static BoatResponse ToResponse(Boat boat, BoatOwnerProfile? owner = null) => new BoatResponse
    {
        Id = boat.Id,
        Name = boat.Name,
        Description = boat.Description,
        Capacity = boat.Capacity,
        OwnerProfileId = boat.OwnerProfileId,
        OwnerName = owner != null ? $"{owner.User?.FirstName} {owner.User?.LastName}" : 
                    (boat.OwnerProfile?.User != null ? $"{boat.OwnerProfile.User.FirstName} {boat.OwnerProfile.User.LastName}" : "Unknown"),
        ImageUrls = boat.Images.Select(img => img.ImageUrl).ToList()
    };
}
