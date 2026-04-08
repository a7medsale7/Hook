using Hook.Application.Contracts.Payment;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions.Repositories;
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
public class PaymentsController(
    IPaymentService paymentService,
    IBoatOwnerRepository boatOwnerRepository) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IBoatOwnerRepository _boatOwnerRepository = boatOwnerRepository;

    [HttpGet("my")]
    [Authorize(Policy = Permissions.Payments_View)]
    public async Task<IActionResult> GetMyPayments(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _paymentService.GetMyPaymentsAsync(userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{id}/receipt")]
    [Authorize(Policy = Permissions.Payments_UploadReceipt)]
    public async Task<IActionResult> UploadReceipt(Guid id, [FromForm] UploadReceiptRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _paymentService.UploadReceiptAsync(id, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPost("{id}/verify")]
    [Authorize(Policy = Permissions.Payments_Verify)] 
    public async Task<IActionResult> VerifyPayment(Guid id, VerifyPaymentRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        // The service handles logic, but controller can also check role if needed
        var result = await _paymentService.VerifyPaymentAsync(id, userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("owner/all")]
    [Authorize(Policy = Permissions.Payments_View)]
    public async Task<IActionResult> GetOwnerPayments([FromQuery] PaymentFilterRequest filter, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        
        if (ownerProfile == null)
            return Unauthorized();

        var result = await _paymentService.GetFilteredPaymentsAsync(filter, ownerId: ownerProfile.Id, cancellationToken: cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("stats")]
    [Authorize(Policy = Permissions.Payments_Stats)]
    public async Task<IActionResult> GetStats(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var isAdmin = User.IsInRole(DefaultRoles.Admin);
        
        Guid? ownerId = null;
        if (!isAdmin)
        {
            var ownerProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
            if (ownerProfile != null)
                ownerId = ownerProfile.Id;
        }

        // If they are Admin or Owner, they don't filter by UserId. 
        // Admin sees all, Owner sees own trips by ownerId.
        string? filterUserId = (!isAdmin && ownerId == null) ? userId : null;

        var result = await _paymentService.GetFinancialStatsAsync(filterUserId, ownerId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
