using Hook.Application.Contracts.Review;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Hook.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpPost]
    [Authorize(Policy = Permissions.Reviews_Create)]
    public async Task<IActionResult> Create(CreateReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _reviewService.CreateAsync(userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Reviews_Update)]
    public async Task<IActionResult> Update(Guid id, UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _reviewService.UpdateAsync(id, userId, request, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("trip/{tripId}")]
    // No permission needed for viewing trip reviews, they are public
    public async Task<IActionResult> GetTripReviews(Guid tripId, CancellationToken cancellationToken)
    {
        var result = await _reviewService.GetTripReviewsAsync(tripId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my-reviews")]
    [Authorize(Policy = Permissions.Reviews_View)]
    public async Task<IActionResult> GetMyReviews(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _reviewService.GetMyReviewsAsync(userId, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Reviews_Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await _reviewService.DeleteAsync(id, userId, cancellationToken);
        
        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}
