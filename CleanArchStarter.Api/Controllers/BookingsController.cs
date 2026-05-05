using Hook.Application.Contracts.Booking;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost("user/create")]
    [Authorize(Policy = Permissions.Bookings_Create)]
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _bookingService.CreateBookingAsync(userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("user/my-bookings")]
    [Authorize(Policy = Permissions.Bookings_View)]
    public async Task<IActionResult> GetMyBookings(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _bookingService.GetMyBookingsAsync(userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("allroles/search")]
    [Authorize(Policy = Permissions.Bookings_View)]
    public async Task<IActionResult> SearchBookings([FromQuery] BookingFilterRequest filter, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var isOwner = User.IsInRole(DefaultRoles.BoatOwner);

        // Logic: Users see only their bookings. Owners see bookings for their boats. Admins see all.
        // We'll pass correct IDs to the service.
        var result = await _bookingService.GetFilteredBookingsAsync(
            filter, 
            userId: isAdmin ? null : userId, 
            ownerId: null, 
            ownerUserId: null,
            cancellationToken: cancellationToken);
            
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("boatowner/GetAll")]
    [Authorize(Policy = Permissions.Bookings_View)]
    public async Task<IActionResult> GetOwnerBookings([FromQuery] BookingFilterRequest filter, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        
        // We pass the userId to a service method that knows how to find owner bookings
        var result = await _bookingService.GetFilteredBookingsAsync(filter, userId: null, ownerId: null, ownerUserId: userId, cancellationToken: cancellationToken); 
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("boatowner/stats")]
    [Authorize(Policy = Permissions.Bookings_View)]
    public async Task<IActionResult> GetStats(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        var result = await _bookingService.GetBookingStatsAsync(userId, isAdmin, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("allroles/trip-bookings/{dateId}")]
    [Authorize(Policy = Permissions.Bookings_View)]
    public async Task<IActionResult> GetTripBookings(Guid dateId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _bookingService.GetTripBookingsAsync(dateId, userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("admin/GetAll")]
    [Authorize(Policy = Permissions.Bookings_ViewAll)]
    public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken)
    {
        var result = await _bookingService.GetAllBookingsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPatch("boatowner/update-status/{id}")]
    [Authorize(Policy = Permissions.Bookings_UpdateStatus)]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateBookingStatusRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _bookingService.UpdateBookingStatusAsync(id, userId, request, false, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("user/cancel/{id}")]
    [Authorize(Policy = Permissions.Bookings_Cancel)]
    public async Task<IActionResult> CancelBooking(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _bookingService.CancelBookingAsync(id, userId, cancellationToken);
        
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    [HttpDelete("admin/hard-delete/{id}")]
    [Authorize(Policy = Permissions.Bookings_Delete)]
    public async Task<IActionResult> HardDeleteBooking(Guid id, CancellationToken cancellationToken)
    {
        // Permission check is already done by [Authorize] and Policy
        var result = await _bookingService.HardDeleteBookingAsync(id, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}
