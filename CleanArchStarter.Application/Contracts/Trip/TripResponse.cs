using System;
using System.Collections.Generic;

namespace Hook.Application.Contracts.Trip;

public record TripResponse(
    Guid Id,
    string Title,
    string ShortDescription,
    string DetailedDescription,
    string LocationName,
    string Address,
    double Latitude,
    double Longitude,
    decimal PricePerPerson,
    int MaxParticipants,
    bool IsGuided,
    bool HasEquipmentRental,
    bool HasSnorkeling,
    Guid BoatId,
    string BoatName,
    Guid TripManagerId,
    string TripManagerName,
    List<TripImageResponse> Images,
    string? MainImageUrl,
    List<TripDateResponse> TripDates,
    string? TripManagerImageUrl = null,
    Hook.Application.Contracts.Boat.BoatResponse? Boat = null
);

public record TripImageResponse(
    Guid Id,
    string ImageUrl,
    bool IsMainImage
);
