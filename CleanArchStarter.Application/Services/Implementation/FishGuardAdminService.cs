using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.FishGuard.Admin;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hook.Application.Services.Implementation;

public class FishGuardAdminService : IFishGuardAdminService
{
    private readonly ApplicationDbContext _context;

    public FishGuardAdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    // --- Restricted Locations ---
    public async Task<Result<IEnumerable<RestrictedLocation>>> GetLocationsAsync(CancellationToken cancellationToken = default)
    {
        var locations = await _context.RestrictedLocations.AsNoTracking().ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<RestrictedLocation>>(locations);
    }

    public async Task<Result<RestrictedLocation>> GetLocationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var location = await _context.RestrictedLocations.FindAsync(new object[] { id }, cancellationToken);
        if (location == null) return Result.Failure<RestrictedLocation>(new Error("NotFound", "RestrictedLocation not found"));
        return Result.Success(location);
    }

    public async Task<Result<RestrictedLocation>> CreateLocationAsync(CreateRestrictedLocationDto request, CancellationToken cancellationToken = default)
    {
        var location = request.Adapt<RestrictedLocation>();
        
        _context.RestrictedLocations.Add(location);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(location);
    }

    public async Task<Result<RestrictedLocation>> UpdateLocationAsync(int id, UpdateRestrictedLocationDto request, CancellationToken cancellationToken = default)
    {
        var location = await _context.RestrictedLocations.FindAsync(new object[] { id }, cancellationToken);
        if (location == null) return Result.Failure<RestrictedLocation>(new Error("NotFound", "RestrictedLocation not found"));

        request.Adapt(location);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(location);
    }

    public async Task<Result> DeleteLocationAsync(int id, CancellationToken cancellationToken = default)
    {
        var location = await _context.RestrictedLocations.FindAsync(new object[] { id }, cancellationToken);
        if (location == null) return Result.Failure(new Error("NotFound", "RestrictedLocation not found"));

        _context.RestrictedLocations.Remove(location);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> ImportLocationsAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
            return Result.Failure(new Error("ValidationError", "No file uploaded or file is empty"));

        try
        {
            using var stream = file.OpenReadStream();
            var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var requests = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<CreateRestrictedLocationDto>>(stream, options, cancellationToken);

            if (requests == null || !requests.Any())
                return Result.Failure(new Error("ValidationError", "The file contains no valid location data"));

            var locations = requests.Select(r => r.Adapt<RestrictedLocation>()).ToList();

            _context.RestrictedLocations.AddRange(locations);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (System.Text.Json.JsonException)
        {
            return Result.Failure(new Error("ValidationError", "Invalid JSON format. Please upload a valid JSON array of locations."));
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("ServerError", $"Error importing file: {ex.Message}"));
        }
    }

    // --- Restricted Tools ---
    public async Task<Result<IEnumerable<RestrictedTool>>> GetToolsAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.RestrictedTools.AsNoTracking().ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<RestrictedTool>>(items);
    }

    public async Task<Result<RestrictedTool>> GetToolByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.RestrictedTools.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<RestrictedTool>(new Error("NotFound", "RestrictedTool not found"));
        return Result.Success(item);
    }

    public async Task<Result<RestrictedTool>> CreateToolAsync(CreateRestrictedToolDto request, CancellationToken cancellationToken = default)
    {
        var item = request.Adapt<RestrictedTool>();
        
        _context.RestrictedTools.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result<RestrictedTool>> UpdateToolAsync(int id, UpdateRestrictedToolDto request, CancellationToken cancellationToken = default)
    {
        var item = await _context.RestrictedTools.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<RestrictedTool>(new Error("NotFound", "RestrictedTool not found"));

        request.Adapt(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result> DeleteToolAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.RestrictedTools.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure(new Error("NotFound", "RestrictedTool not found"));

        _context.RestrictedTools.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    // --- Fishing Seasons ---
    public async Task<Result<IEnumerable<FishingSeason>>> GetSeasonsAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.FishingSeasons.AsNoTracking().ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<FishingSeason>>(items);
    }

    public async Task<Result<FishingSeason>> GetSeasonByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingSeasons.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<FishingSeason>(new Error("NotFound", "FishingSeason not found"));
        return Result.Success(item);
    }

    public async Task<Result<FishingSeason>> CreateSeasonAsync(CreateFishingSeasonDto request, CancellationToken cancellationToken = default)
    {
        var item = request.Adapt<FishingSeason>();
        
        _context.FishingSeasons.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result<FishingSeason>> UpdateSeasonAsync(int id, UpdateFishingSeasonDto request, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingSeasons.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<FishingSeason>(new Error("NotFound", "FishingSeason not found"));

        request.Adapt(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result> DeleteSeasonAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingSeasons.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure(new Error("NotFound", "FishingSeason not found"));

        _context.FishingSeasons.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    // --- Fishing FAQs ---
    public async Task<Result<IEnumerable<FishingFaq>>> GetFaqsAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.FishingFaqs.AsNoTracking().ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<FishingFaq>>(items);
    }

    public async Task<Result<FishingFaq>> GetFaqByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingFaqs.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<FishingFaq>(new Error("NotFound", "FishingFaq not found"));
        return Result.Success(item);
    }

    public async Task<Result<FishingFaq>> CreateFaqAsync(CreateFishingFaqDto request, CancellationToken cancellationToken = default)
    {
        var item = request.Adapt<FishingFaq>();
        
        _context.FishingFaqs.Add(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result<FishingFaq>> UpdateFaqAsync(int id, UpdateFishingFaqDto request, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingFaqs.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure<FishingFaq>(new Error("NotFound", "FishingFaq not found"));

        request.Adapt(item);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success(item);
    }

    public async Task<Result> DeleteFaqAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.FishingFaqs.FindAsync(new object[] { id }, cancellationToken);
        if (item == null) return Result.Failure(new Error("NotFound", "FishingFaq not found"));

        _context.FishingFaqs.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
