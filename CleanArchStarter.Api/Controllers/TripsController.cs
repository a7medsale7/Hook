using Hook.Application.Contracts.Trip;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController(ITripService tripService) : ControllerBase
{
    private readonly ITripService _tripService = tripService;

    [HttpPost]
    [Authorize(Policy = Permissions.Trips_Create)]
    public async Task<IActionResult> Create([FromForm] CreateTripRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _tripService.CreateTripAsync(userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _tripService.GetByIdAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _tripService.GetAllAsync(pageNumber, pageSize, cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] string? query, 
        [FromQuery] string? locationName, 
        [FromQuery] DateTime? date, 
        [FromQuery] int? participants, 
        [FromQuery] decimal? minPrice, 
        [FromQuery] decimal? maxPrice, 
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10, 
        CancellationToken cancellationToken = default)
    {
        var result = await _tripService.SearchTripsAsync(query, locationName, date, participants, minPrice, maxPrice, pageNumber, pageSize, cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("my-trips")]
    [Authorize(Policy = Permissions.Trips_View)]
    public async Task<IActionResult> GetMyTrips(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _tripService.GetMyTripsAsync(userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Trips_Update)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateTripRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var result = await _tripService.UpdateTripAsync(id, userId, request, isAdmin, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Trips_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var result = await _tripService.SoftDeleteTripAsync(id, userId, isAdmin, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("{id}/dates")]
    [Authorize(Policy = Permissions.Trips_Update)]
    public async Task<IActionResult> AddDates(Guid id, [FromBody] AddTripDatesRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _tripService.AddTripDatesAsync(id, userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPatch("dates/{dateId}")]
    [Authorize(Policy = Permissions.Trips_Update)]
    public async Task<IActionResult> ToggleDateStatus(Guid dateId, [FromQuery] bool isActive, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _tripService.ToggleTripDateStatusAsync(dateId, userId, isActive, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
