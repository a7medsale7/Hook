using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Hook.Application.Contracts.Trip;

public record CreateTripRequest(
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
    List<IFormFile> Images
);
