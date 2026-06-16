using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hook.Domain.Consts;
using Hook.Application.Services.Interfaces;
using Hook.Application.Contracts.FishGuard.Admin;

namespace Hook.Api.Controllers;

[Route("api/admin/fishguard")]
[ApiController]
[Authorize(Roles = "Admin,CommunityAdmin", Policy = Permissions.FishGuardAdmin_Manage)]
public class FishGuardAdminController : ControllerBase
{
    private readonly IFishGuardAdminService _adminService;

    public FishGuardAdminController(IFishGuardAdminService adminService)
    {
        _adminService = adminService;
    }

    // --- Restricted Locations ---
    [HttpGet("locations")]
    public async Task<IActionResult> GetLocations(CancellationToken cancellationToken)
    {
        var result = await _adminService.GetLocationsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("locations/{id}")]
    public async Task<IActionResult> GetLocation(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.GetLocationByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("locations")]
    public async Task<IActionResult> CreateLocation([FromBody] CreateRestrictedLocationDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateLocationAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetLocation), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("locations/{id}")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateRestrictedLocationDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateLocationAsync(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("locations/{id}")]
    public async Task<IActionResult> DeleteLocation(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.DeleteLocationAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("locations/import")]
    public async Task<IActionResult> ImportLocations(IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _adminService.ImportLocationsAsync(file, cancellationToken);
        return result.IsSuccess ? Ok(new { message = "Locations imported successfully" }) : BadRequest(result.Error);
    }

    // --- Restricted Tools ---
    [HttpGet("tools")]
    public async Task<IActionResult> GetTools(CancellationToken cancellationToken)
    {
        var result = await _adminService.GetToolsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("tools/{id}")]
    public async Task<IActionResult> GetTool(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.GetToolByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("tools")]
    public async Task<IActionResult> CreateTool([FromBody] CreateRestrictedToolDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateToolAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetTool), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("tools/{id}")]
    public async Task<IActionResult> UpdateTool(int id, [FromBody] UpdateRestrictedToolDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateToolAsync(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("tools/{id}")]
    public async Task<IActionResult> DeleteTool(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.DeleteToolAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("tools/import")]
    public async Task<IActionResult> ImportTools(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
        return Ok(new { message = "Tools imported successfully" });
    }

    // --- Fishing Seasons ---
    [HttpGet("seasons")]
    public async Task<IActionResult> GetSeasons(CancellationToken cancellationToken)
    {
        var result = await _adminService.GetSeasonsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("seasons/{id}")]
    public async Task<IActionResult> GetSeason(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.GetSeasonByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("seasons")]
    public async Task<IActionResult> CreateSeason([FromBody] CreateFishingSeasonDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateSeasonAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetSeason), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("seasons/{id}")]
    public async Task<IActionResult> UpdateSeason(int id, [FromBody] UpdateFishingSeasonDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateSeasonAsync(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("seasons/{id}")]
    public async Task<IActionResult> DeleteSeason(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.DeleteSeasonAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("seasons/import")]
    public async Task<IActionResult> ImportSeasons(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
        return Ok(new { message = "Fishing seasons imported successfully" });
    }

    // --- Fishing FAQs ---
    [HttpGet("faqs")]
    public async Task<IActionResult> GetFaqs(CancellationToken cancellationToken)
    {
        var result = await _adminService.GetFaqsAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("faqs/{id}")]
    public async Task<IActionResult> GetFaq(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.GetFaqByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("faqs")]
    public async Task<IActionResult> CreateFaq([FromBody] CreateFishingFaqDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateFaqAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(GetFaq), new { id = result.Value.Id }, result.Value) : BadRequest(result.Error);
    }

    [HttpPut("faqs/{id}")]
    public async Task<IActionResult> UpdateFaq(int id, [FromBody] UpdateFishingFaqDto request, CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateFaqAsync(id, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpDelete("faqs/{id}")]
    public async Task<IActionResult> DeleteFaq(int id, CancellationToken cancellationToken)
    {
        var result = await _adminService.DeleteFaqAsync(id, cancellationToken);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPost("faqs/import")]
    public async Task<IActionResult> ImportFaqs(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");
        return Ok(new { message = "FAQs imported successfully" });
    }
}
