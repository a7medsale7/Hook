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
    IBookingRepository bookingRepository,
    IFileService fileService,
    IUnitOfWork unitOfWork,
    IFuzzySearchService fuzzySearchService) : ITripService
{
    private readonly ITripRepository _tripRepository = tripRepository;
    private readonly ITripDateRepository _tripDateRepository = tripDateRepository;
    private readonly IBoatRepository _boatRepository = boatRepository;
    private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IFileService _fileService = fileService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFuzzySearchService _fuzzySearchService = fuzzySearchService;

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

        // 2.5 Check for Restricted Fishing Locations
        var fishingKeywords = new[] { "صيد", "سمك", "سنارة", "تسقيط", "جيجينج", "fishing", "fish", "طعم", "جروف" };
        var combinedText = $"{request.Title} {request.ShortDescription} {request.DetailedDescription}";
        bool isFishingTrip = fishingKeywords.Any(k => combinedText.Contains(k, StringComparison.OrdinalIgnoreCase));

        if (isFishingTrip && !string.IsNullOrWhiteSpace(request.LocationName))
        {
            // Map comprehensive English location names to Arabic for the DB search
            var locationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "ras mohammed", "رأس محمد" }, { "ras mohamed", "رأس محمد" },
                { "giftun", "الجفتون" }, { "geftun", "الجفتون" },
                { "abu galum", "أبو جالوم" },
                { "nabq", "نبق" },
                { "tiran", "تيران" },
                { "sanafir", "صنافير" },
                { "wadi el gemal", "وادي الجمال" }, { "wadi elgemal", "وادي الجمال" },
                { "brother islands", "الأخوين" }, { "brothers islands", "الأخوين" },
                { "daedalus", "ديدالوس" },
                { "elphinstone", "الفينستون" },
                { "taba", "طابا" },
                { "saint catherine", "سانت كاترين" }, { "st catherine", "سانت كاترين" },
                { "gebel elba", "جبل علبة" }, { "elba", "جبل علبة" },
                { "el omaid", "العميد" },
                { "salum", "السلوم" }, { "salloum", "السلوم" },
                { "wadi allaqi", "وادي العلاقي" },
                { "qarun", "قارون" }, { "karun", "قارون" },
                { "wadi el rayan", "وادي الريان" }, { "wadi rayan", "وادي الريان" },
                { "ashtoum el gamil", "أشتوم الجميل" },
                { "burullus", "البرلس" },
                { "zaranik", "الزرانيق" },
                { "ahrash", "الأحراش" },
                { "petrified forest", "الغابة المتحجرة" },
                { "white desert", "الصحراء البيضاء" },
                { "siwa", "سيوة" },
                { "alamein", "العلمين" }, { "el alamein", "العلمين" },
                { "sannur", "كهف وادي سنور" },
                { "rosetta", "بوغاز رشيد" }, { "rashid", "بوغاز رشيد" },
                { "damietta", "بوغاز دمياط" }, { "dumyat", "بوغاز دمياط" },
                { "manzala", "المنزلة" },
                { "bardawil", "البردويل" },
                { "edko", "إدكو" }, { "idku", "إدكو" },
                { "mariout", "مريوط" }, { "maryut", "مريوط" },
                { "lake nasser", "بحيرة ناصر" }, { "nasser lake", "بحيرة ناصر" },
                { "abu simbel", "أبو سمبل" },
                { "montaza", "المنتزه" }, { "montazah", "المنتزه" },
                { "maamoura", "المعمورة" },
                { "sharm el sheikh", "شرم الشيخ" },
                { "naama bay", "خليج نعمة" },
                { "marsa alam", "مرسى علم" }
            };

            string searchLocation = request.LocationName.Trim();
            foreach (var kvp in locationMap)
            {
                if (searchLocation.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    searchLocation = kvp.Value;
                    break;
                }
            }

            var searchResult = await _fuzzySearchService.SearchAsync(Hook.Domain.Enums.ChatCategory.RestrictedLocation, searchLocation, cancellationToken);
            if (searchResult.Entity != null)
            {
                return Result.Failure<TripResponse>(new Error("Trip.RestrictedLocation", $"Sorry, you cannot organize a fishing trip in '{request.LocationName}' because it is designated as a protected area or fishing is legally prohibited there."));
            }
        }

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

        // 0.5 Check for Restricted Fishing Locations
        var fishingKeywords = new[] { "صيد", "سمك", "سنارة", "تسقيط", "جيجينج", "fishing", "fish", "طعم", "جروف" };
        var combinedText = $"{request.Title} {request.ShortDescription} {request.DetailedDescription}";
        bool isFishingTrip = fishingKeywords.Any(k => combinedText.Contains(k, StringComparison.OrdinalIgnoreCase));

        if (isFishingTrip && !string.IsNullOrWhiteSpace(request.LocationName))
        {
            // Map comprehensive English location names to Arabic for the DB search
            var locationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "ras mohammed", "رأس محمد" }, { "ras mohamed", "رأس محمد" },
                { "giftun", "الجفتون" }, { "geftun", "الجفتون" },
                { "abu galum", "أبو جالوم" },
                { "nabq", "نبق" },
                { "tiran", "تيران" },
                { "sanafir", "صنافير" },
                { "wadi el gemal", "وادي الجمال" }, { "wadi elgemal", "وادي الجمال" },
                { "brother islands", "الأخوين" }, { "brothers islands", "الأخوين" },
                { "daedalus", "ديدالوس" },
                { "elphinstone", "الفينستون" },
                { "taba", "طابا" },
                { "saint catherine", "سانت كاترين" }, { "st catherine", "سانت كاترين" },
                { "gebel elba", "جبل علبة" }, { "elba", "جبل علبة" },
                { "el omaid", "العميد" },
                { "salum", "السلوم" }, { "salloum", "السلوم" },
                { "wadi allaqi", "وادي العلاقي" },
                { "qarun", "قارون" }, { "karun", "قارون" },
                { "wadi el rayan", "وادي الريان" }, { "wadi rayan", "وادي الريان" },
                { "ashtoum el gamil", "أشتوم الجميل" },
                { "burullus", "البرلس" },
                { "zaranik", "الزرانيق" },
                { "ahrash", "الأحراش" },
                { "petrified forest", "الغابة المتحجرة" },
                { "white desert", "الصحراء البيضاء" },
                { "siwa", "سيوة" },
                { "alamein", "العلمين" }, { "el alamein", "العلمين" },
                { "sannur", "كهف وادي سنور" },
                { "rosetta", "بوغاز رشيد" }, { "rashid", "بوغاز رشيد" },
                { "damietta", "بوغاز دمياط" }, { "dumyat", "بوغاز دمياط" },
                { "manzala", "المنزلة" },
                { "bardawil", "البردويل" },
                { "edko", "إدكو" }, { "idku", "إدكو" },
                { "mariout", "مريوط" }, { "maryut", "مريوط" },
                { "lake nasser", "بحيرة ناصر" }, { "nasser lake", "بحيرة ناصر" },
                { "abu simbel", "أبو سمبل" },
                { "montaza", "المنتزه" }, { "montazah", "المنتزه" },
                { "maamoura", "المعمورة" },
                { "sharm el sheikh", "شرم الشيخ" },
                { "naama bay", "خليج نعمة" },
                { "marsa alam", "مرسى علم" }
            };

            string searchLocation = request.LocationName.Trim();
            foreach (var kvp in locationMap)
            {
                if (searchLocation.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                {
                    searchLocation = kvp.Value;
                    break;
                }
            }

            var searchResult = await _fuzzySearchService.SearchAsync(Hook.Domain.Enums.ChatCategory.RestrictedLocation, searchLocation, cancellationToken);
            if (searchResult.Entity != null)
            {
                return Result.Failure<TripResponse>(new Error("Trip.RestrictedLocation", $"Sorry, you cannot organize a fishing trip in '{request.LocationName}' because it is designated as a protected area or fishing is legally prohibited there."));
            }
        }

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

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(ToResponse(trip));
    }

    public async Task<Result<TripResponse>> UpdateImagesAsync(Guid id, string userId, Hook.Application.Contracts.Common.UpdateImagesRequest request, bool isAdmin = false, CancellationToken cancellationToken = default)
    {
        var trip = await _tripRepository.GetByIdWithDetailsAsync(id);
        if (trip is null)
            return Result.Failure<TripResponse>(TripErrors.NotFound);

        // Check ownership (bypass for Admin)
        if (!isAdmin && trip.TripManager.UserId != userId)
            return Result.Failure<TripResponse>(TripErrors.Unauthorized);

        // 1. Handle Image Deletions
        if (request.ImageIdsToDelete != null && request.ImageIdsToDelete.Any())
        {
            var imagesToDelete = trip.Images.Where(img => request.ImageIdsToDelete.Contains(img.Id)).ToList();
            foreach (var img in imagesToDelete)
            {
                _fileService.DeleteFile(img.ImageUrl);
                img.IsDeleted = true;
                img.IsMainImage = false;
            }
        }

        // 2. Handle New Images - Use repository AddImageAsync to explicitly mark as Added
        if (request.NewImages != null && request.NewImages.Any())
        {
            var newUrls = await _fileService.SaveFilesAsync(request.NewImages, "uploads/trips");
            foreach (var url in newUrls)
            {
                await _tripRepository.AddImageAsync(new TripImage { ImageUrl = url, TripId = trip.Id });
            }
        }

        // 3. Handle Main Image update
        var activeImages = trip.Images.Where(i => !i.IsDeleted).ToList();

        if (request.MainImageId.HasValue)
        {
            foreach (var img in activeImages)
            {
                img.IsMainImage = (img.Id == request.MainImageId.Value);
            }
        }

        if (activeImages.Any() && !activeImages.Any(i => i.IsMainImage))
        {
            activeImages.First().IsMainImage = true;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Reload fresh data from DB to ensure response matches reality
        var updatedTrip = await _tripRepository.GetByIdWithDetailsAsync(id);
        return Result.Success(ToResponse(updatedTrip!));
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

    public async Task<Result> RestoreTripAsync(Guid id, string userId, CancellationToken cancellationToken = default)
    {
        var trip = await _tripRepository.GetDeletedByIdAsync(id);
        if (trip is null)
            return Result.Failure(TripErrors.NotFound);

        if (trip.TripManager.UserId != userId)
            return Result.Failure(TripErrors.Unauthorized);

        trip.IsDeleted = false;
        _tripRepository.Update(trip);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<TripResponse>>> GetMyDeletedTripsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (ownerProfile is null)
            return Result.Failure<IEnumerable<TripResponse>>(TripErrors.NoBoatAvailable);

        var trips = await _tripRepository.GetDeletedByOwnerIdAsync(ownerProfile.Id);
        return Result.Success(trips.Select(t => ToResponse(t)));
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
                EndDate = dateDto.StartDate.AddDays(1),
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

        if (!isActive)
        {
            var bookings = await _bookingRepository.GetByTripDateIdAsync(dateId);
            var unrefundedBookings = bookings.Where(b => 
                !b.IsDeleted && 
                b.Payment != null && 
                b.Payment.Status != PaymentStatus.Refunded && 
                b.Payment.Status != PaymentStatus.Rejected && 
                b.Payment.Status != PaymentStatus.Failed).ToList();

            if (unrefundedBookings.Any())
                return Result.Failure(TripErrors.DateHasUnrefundedBookings);
        }

        tripDate.IsActive = isActive;
        _tripDateRepository.Update(tripDate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteTripDateAsync(Guid dateId, string userId, CancellationToken cancellationToken = default)
    {
        var tripDate = await _tripDateRepository.GetByIdAsync(dateId);
        if (tripDate is null)
            return Result.Failure(TripErrors.DateNotFound);

        // Ownership check
        if (tripDate.Trip.TripManager.UserId != userId)
            return Result.Failure(TripErrors.Unauthorized);

        // Check for bookings
        var bookings = await _bookingRepository.GetByTripDateIdAsync(dateId);
        
        // منع الحذف إذا وجد أي عملية دفع لم يتم ردها (Refunded) أو رفضها (Rejected) أو فشلها (Failed)
        // هذا يشمل الحجوزات المؤكدة، وحتى الملغاة التي لم يسترد أصحابها أموالهم بعد
        var unrefundedBookings = bookings.Where(b => 
            !b.IsDeleted && 
            b.Payment != null && 
            b.Payment.Status != PaymentStatus.Refunded && 
            b.Payment.Status != PaymentStatus.Rejected && 
            b.Payment.Status != PaymentStatus.Failed).ToList();

        if (unrefundedBookings.Any())
            return Result.Failure(TripErrors.DateHasUnrefundedBookings);

        // 1. مسح كل الحجوزات المرتبطة (وبالتالي سيتم مسح المدفوعات تلقائياً)
        foreach (var booking in bookings)
        {
            _bookingRepository.HardDelete(booking);
        }

        // 2. مسح التاريخ نهائياً
        _tripDateRepository.HardDelete(tripDate);
        
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
        trip.Images.Where(i => !i.IsDeleted).Select(i => new TripImageResponse(i.Id, i.ImageUrl, i.IsMainImage)).ToList(),
        trip.Images.Where(i => !i.IsDeleted).FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? trip.Images.Where(i => !i.IsDeleted).FirstOrDefault()?.ImageUrl,
        trip.TripDates.Select(d => new TripDateResponse(d.Id, d.StartDate, d.EndDate, d.AvailableSeats, d.IsActive)).ToList(),
        trip.TripManager?.User?.ProfilePictureUrl,
        trip.Boat == null ? null : new Hook.Application.Contracts.Boat.BoatResponse
        {
            Id = trip.Boat.Id,
            Name = trip.Boat.Name,
            Description = trip.Boat.Description,
            Capacity = trip.Boat.Capacity,
            OwnerProfileId = trip.Boat.OwnerProfileId,
            OwnerName = trip.Boat.OwnerProfile?.User != null ? $"{trip.Boat.OwnerProfile.User.FirstName} {trip.Boat.OwnerProfile.User.LastName}" : "Unknown",
            Images = trip.Boat.Images.Select(i => new Hook.Application.Contracts.Boat.BoatImageResponse
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                IsMainImage = i.IsMainImage
            }).ToList(),
            MainImageUrl = trip.Boat.Images.FirstOrDefault(i => i.IsMainImage)?.ImageUrl ?? trip.Boat.Images.FirstOrDefault()?.ImageUrl
        },
        trip.TripManager?.InstaPayNumber,
        trip.TripManager?.VodafoneCashNumber,
        trip.TripManager?.User?.PhoneNumber
    );
}
