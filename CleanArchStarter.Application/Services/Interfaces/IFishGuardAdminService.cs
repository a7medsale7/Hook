using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.FishGuard.Admin;
using Hook.Domain.Entities;

namespace Hook.Application.Services.Interfaces;

public interface IFishGuardAdminService
{
    // Restricted Locations
    Task<Result<IEnumerable<RestrictedLocation>>> GetLocationsAsync(CancellationToken cancellationToken = default);
    Task<Result<RestrictedLocation>> GetLocationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<RestrictedLocation>> CreateLocationAsync(CreateRestrictedLocationDto request, CancellationToken cancellationToken = default);
    Task<Result<RestrictedLocation>> UpdateLocationAsync(int id, UpdateRestrictedLocationDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteLocationAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> ImportLocationsAsync(Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken = default);

    // Restricted Tools
    Task<Result<IEnumerable<RestrictedTool>>> GetToolsAsync(CancellationToken cancellationToken = default);
    Task<Result<RestrictedTool>> GetToolByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<RestrictedTool>> CreateToolAsync(CreateRestrictedToolDto request, CancellationToken cancellationToken = default);
    Task<Result<RestrictedTool>> UpdateToolAsync(int id, UpdateRestrictedToolDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteToolAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> ImportToolsAsync(Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken = default);

    // Fishing Seasons
    Task<Result<IEnumerable<FishingSeason>>> GetSeasonsAsync(CancellationToken cancellationToken = default);
    Task<Result<FishingSeason>> GetSeasonByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<FishingSeason>> CreateSeasonAsync(CreateFishingSeasonDto request, CancellationToken cancellationToken = default);
    Task<Result<FishingSeason>> UpdateSeasonAsync(int id, UpdateFishingSeasonDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteSeasonAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> ImportSeasonsAsync(Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken = default);

    // Fishing FAQs
    Task<Result<IEnumerable<FishingFaq>>> GetFaqsAsync(CancellationToken cancellationToken = default);
    Task<Result<FishingFaq>> GetFaqByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<FishingFaq>> CreateFaqAsync(CreateFishingFaqDto request, CancellationToken cancellationToken = default);
    Task<Result<FishingFaq>> UpdateFaqAsync(int id, UpdateFishingFaqDto request, CancellationToken cancellationToken = default);
    Task<Result> DeleteFaqAsync(int id, CancellationToken cancellationToken = default);
}
