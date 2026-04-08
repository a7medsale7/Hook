using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Trip;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class TripService(
    ITripRepository tripRepository,
    ITripDateRepository tripDateRepository,
    IBoatRepository boatRepository,
    IBoatOwnerRepository boatOwnerRepository,
    IFileService fileService,
    IUnitOfWork unitOfWork) : ITripService
{
    private readonly ITripRepository _tripRepository = tripRepository;
    private readonly ITripDateRepository _tripDateRepository = tripDateRepository;
    private readonly IBoatRepository _boatRepository = boatRepository;
    private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
    private readonly IFileService _fileService = fileService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<TripResponse>> CreateTripAsync(string userId, CreateTripRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Check if user is an approved boat owner
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<TripResponse>(TripErrors.NoBoatAvailable);

        if (ownerProfile.Status != RequestStatus.Approved)
            return Result.Failure<TripResponse>(BoatErrors.NotApproved);

        // 2. Verify boat ownership and approval
        var boat = await _boatRepository.GetByIdAsync(request.BoatId);
        if (boat is null || boat.OwnerProfileId != ownerProfile.Id)
            return Result.Failure<TripResponse>(TripErrors.BoatNotOwned);

        // 3. Upload Images
        var imageUrls = await _fileService.SaveFilesAsync(request.Images, "uploads/trips");

        var tripsImages = imageUrls.Select((url, index) => new TripImage 
        { 
            ImageUrl = url, 
            IsMainImage = index == request.MainImageIndex 
        }).ToList();

        // 4. Create Trip
        var trip = new Trip
        {
            Title = request.Title,
            ShortDescription = request.ShortDescription,
            DetailedDescription = request.DetailedDescription,
            LocationName = request.LocationName,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            PricePerPerson = request.PricePerPerson,
            MaxParticipants = request.MaxParticipants,
            IsGuided = request.IsGuided,
            HasEquipmentRental = request.HasEquipmentRental,
            HasSnorkeling = request.HasSnorkeling,
            BoatId = request.BoatId,
            TripManagerId = ownerProfile.Id,
            Images = tripsImages
        };

        await _tripRepository.AddAsync(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Fetch details for ToResponse mapping
        var savedTrip = await _tripRepository.GetByIdWithDetailsAsync(trip.Id);
        return Result.Success(ToResponse(savedTrip!));
    }

    public async Task<Result<TripResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id);
        if (trip is null)
            return Result.Failure<TripResponse>(TripErrors.NotFound);

        return Result.Success(ToResponse(trip));
    }

    public async Task<Result<IEnumerable<TripResponse>>> GetAllAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync();
        var totalCount = trips.Count();
        var pagedTrips = trips.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return Result.Success(pagedTrips.Select(t => ToResponse(t)));
    }

    public async Task<Result<IEnumerable<TripResponse>>> SearchTripsAsync(string? query, string? location, DateTime? date, int? participants, decimal? minPrice, decimal? maxPrice, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var trips = await _tripRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(query))
        {
            trips = trips.Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                                     (t.Boat != null && t.Boat.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));
        }

        if (!string.IsNullOrEmpty(location))
            trips = trips.Where(t => t.LocationName.Contains(location, StringComparison.OrdinalIgnoreCase));

        if (date.HasValue)
            trips = trips.Where(t => t.TripDates.Any(d => d.StartDate.Date == date.Value.Date && d.IsActive));

        if (participants.HasValue)
            trips = trips.Where(t => t.MaxParticipants >= participants.Value);

        if (minPrice.HasValue)
            trips = trips.Where(t => t.PricePerPerson >= minPrice.Value);

        if (maxPrice.HasValue)
            trips = trips.Where(t => t.PricePerPerson <= maxPrice.Value);

        var totalCount = trips.Count();
        var pagedTrips = trips.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return Result.Success(pagedTrips.Select(t => ToResponse(t)));
    }

    public async Task<Result<IEnumerable<TripResponse>>> GetMyTripsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<IEnumerable<TripResponse>>(TripErrors.NoBoatAvailable);

        var trips = await _tripRepository.GetByOwnerIdAsync(ownerProfile.Id);
        return Result.Success(trips.Select(t => ToResponse(t)));
    }

    public async Task<Result<TripResponse>> UpdateTripAsync(Guid id, string userId, UpdateTripRequest request, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id);
        if (trip is null)
            return Result.Failure<TripResponse>(TripErrors.NotFound);

        // Check ownership (bypass for Admin)
        if (!isAdmin && trip.TripManager.UserId != userId)
            return Result.Failure<TripResponse>(TripErrors.Unauthorized);

        // 1. Update basic info
        trip.Title = request.Title;
        trip.ShortDescription = request.ShortDescription;
        trip.DetailedDescription = request.DetailedDescription;
        trip.LocationName = request.LocationName;
        trip.Address = request.Address;
        trip.Latitude = request.Latitude;
        trip.Longitude = request.Longitude;
        trip.PricePerPerson = request.PricePerPerson;
        trip.MaxParticipants = request.MaxParticipants;
        trip.IsGuided = request.IsGuided;
        trip.HasEquipmentRental = request.HasEquipmentRental;
        trip.HasSnorkeling = request.HasSnorkeling;

        // 2. Handle Image Deletions
        if (request.ImageIdsToDelete != null && request.ImageIdsToDelete.Any())
        {
            var imagesToDelete = trip.Images.Where(img => request.ImageIdsToDelete.Contains(img.Id)).ToList();
            foreach (var img in imagesToDelete)
            {
                _fileService.DeleteFile(img.ImageUrl);
                trip.Images.Remove(img);
            }
        }

        // 3. Handle New Images
        if (request.NewImages != null && request.NewImages.Any())
        {
            var newUrls = await _fileService.SaveFilesAsync(request.NewImages, "uploads/trips");
            foreach (var url in newUrls)
            {
                trip.Images.Add(new TripImage { ImageUrl = url });
            }
        }

        // 4. Handle Main Image update
        if (request.MainImageId.HasValue)
        {
            foreach (var img in trip.Images)
            {
                img.IsMainImage = img.Id == request.MainImageId.Value;
            }
        }
        else if (!trip.Images.Any(i => i.IsMainImage) && trip.Images.Any())
        {
            trip.Images.First().IsMainImage = true;
        }

        _tripRepository.Update(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(ToResponse(trip));
    }

    public async Task<Result> SoftDeleteTripAsync(Guid id, string userId, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip is null)
            return Result.Failure(TripErrors.NotFound);

        // Ensure trip manager is loaded for ownership check
        var tripWithDetails = await _tripRepository.GetByIdWithDetailsAsync(id);
        if (!isAdmin && tripWithDetails?.TripManager.UserId != userId)
            return Result.Failure(TripErrors.Unauthorized);

        _tripRepository.Delete(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> AddTripDatesAsync(Guid tripId, string userId, AddTripDatesRequest request, CancellationToken cancellationToken = default)
    {
        var tripWithDetails = await _tripRepository.GetByIdWithDetailsAsync(tripId);
        if (tripWithDetails is null)
            return Result.Failure(TripErrors.NotFound);

        if (tripWithDetails.TripManager.UserId != userId)
            return Result.Failure(TripErrors.Unauthorized);

        foreach (var dateDto in request.Dates)
        {
            var tripDate = new TripDate
            {
                TripId = tripId,
                StartDate = dateDto.StartDate,
                EndDate = dateDto.EndDate,
                AvailableSeats = dateDto.AvailableSeats,
                IsActive = true
            };
            await _tripDateRepository.AddAsync(tripDate);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> ToggleTripDateStatusAsync(Guid dateId, string userId, bool isActive, CancellationToken cancellationToken = default)
    {
        var tripDate = await _tripDateRepository.GetByIdAsync(dateId);
        if (tripDate is null)
            return Result.Failure(TripErrors.DateNotFound);

        if (tripDate.Trip.TripManager.UserId != userId)
            return Result.Failure(TripErrors.Unauthorized);

        tripDate.IsActive = isActive;
        _tripDateRepository.Update(tripDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private TripResponse ToResponse(Trip trip) => new TripResponse(
        trip.Id,
        trip.Title,
        trip.ShortDescription,
        trip.DetailedDescription,
        trip.LocationName,
        trip.Address,
        trip.Latitude,
        trip.Longitude,
        trip.PricePerPerson,
        trip.MaxParticipants,
        trip.IsGuided,
        trip.HasEquipmentRental,
        trip.HasSnorkeling,
        trip.BoatId,
        trip.Boat?.Name ?? "Unknown",
        trip.TripManagerId,
        trip.TripManager?.User != null ? $"{trip.TripManager.User.FirstName} {trip.TripManager.User.LastName}" : "Unknown",
        trip.Images.Select(i => i.ImageUrl).ToList(),
        trip.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? trip.Images.FirstOrDefault()?.ImageUrl,
        trip.TripDates.Select(d => new TripDateResponse(d.Id, d.StartDate, d.EndDate, d.AvailableSeats, d.IsActive)).ToList(),
        trip.Boat == null ? null : new Hook.Application.Contracts.Boat.BoatResponse
        {
            Id = trip.Boat.Id,
            Name = trip.Boat.Name,
            Description = trip.Boat.Description,
            Capacity = trip.Boat.Capacity,
            OwnerProfileId = trip.Boat.OwnerProfileId,
            OwnerName = trip.Boat.OwnerProfile?.User != null ? $"{trip.Boat.OwnerProfile.User.FirstName} {trip.Boat.OwnerProfile.User.LastName}" : "Unknown",
            ImageUrls = trip.Boat.Images.Select(i => i.ImageUrl).ToList(),
            MainImageUrl = trip.Boat.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? trip.Boat.Images.FirstOrDefault()?.ImageUrl
        }
    );
}
