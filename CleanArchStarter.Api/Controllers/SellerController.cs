using Hook.Application.Contracts.Seller;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController(ISellerService sellerService) : ControllerBase
    {
        private readonly ISellerService _sellerService = sellerService;

        [HttpPost("admin-user/apply")]
        [Authorize(Policy = Permissions.Seller_Apply)]
        public async Task<IActionResult> Apply([FromForm] ApplySellerRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _sellerService.ApplyAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("admin-user-seller/profile")]
        [Authorize(Policy = Permissions.Seller_ViewProfile)]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _sellerService.GetProfileAsync(userId, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
        }

        [HttpPut("update-profile")]
        [Authorize(Policy = Permissions.Seller_ViewProfile)]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateSellerProfileRequest request, CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Unauthorized();

            var result = await _sellerService.UpdateProfileAsync(userId, request, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("admin/pending/GetAll")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_ViewAll)]
        public async Task<IActionResult> GetPendingApplications(CancellationToken cancellationToken)
        {
            var result = await _sellerService.GetPendingApplicationsAsync(cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("admin/allroles/GetAll")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_ViewAll)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _sellerService.GetAllAsync(cancellationToken);
            return Ok(result.Value);
        }

        [HttpPost("admin/update-status")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_UpdateStatus)]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateSellerStatusRequest request, CancellationToken cancellationToken)
        {
            var result = await _sellerService.UpdateStatusAsync(request, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("admin/delete/{id}")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_Delete)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sellerService.SoftDeleteAsync(id, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("admin/deleted/GetAll")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_ViewAll)]
        public async Task<IActionResult> GetDeleted(CancellationToken cancellationToken)
        {
            var result = await _sellerService.GetDeletedAsync(cancellationToken);
            return Ok(result.Value);
        }

        [HttpPost("admin/restore/{id}")]
        [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Seller_Restore)]
        public async Task<IActionResult> Restore(Guid id, CancellationToken cancellationToken)
        {
            var result = await _sellerService.RestoreAsync(id, cancellationToken);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }


}
