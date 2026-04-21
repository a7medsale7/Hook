using Hook.Application.Contracts.Users;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hook.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;




    [HttpGet("allroles/profile")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        var result = await _userService.GetProfileAsync(userId);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPut("allroles/profile")]
    [Authorize(Policy = Permissions.Users_UpdateProfile)]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        var result = await _userService.UpdateProfileAsync(userId, request);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("allroles/change-password")]
    [Authorize(Policy = Permissions.Users_ChangePassword)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        var result = await _userService.ChangePasswordAsync(userId, request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("admin/allroles/GetAll")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ViewAll)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost("allroles/forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var originUrl = $"{Request.Scheme}://{Request.Host.Value}";
        await _userService.ForgotPasswordAsync(request, originUrl);
        return Ok(new { message = "If the email address is in the system, a password reset link will be sent to it soon 🙌" });
    }

    [HttpPost("allroles/reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await _userService.ResetPasswordAsync(request);
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpGet("admin/get-by-id/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ViewAll)]
    public async Task<IActionResult> GetUserById([FromRoute] string id)
    {
        var result = await _userService.GetAsync(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }


    [HttpPost("admin")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ManageRoles)]
    public async Task<IActionResult> Add([FromBody] CreateUserReqeust request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }


    // 1. ???????? ????? ?????? ???????? ???????
    [HttpPut("admin/update/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ManageRoles)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, request, cancellationToken);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    // 2. ???????? ????? ???? ???????? (?????/?????)
    [HttpPatch("admin/toggle-status/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ManageRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string id)
    {
        var result = await _userService.ToggleStatus(id);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    // 3. ???????? ?? ??? ???????? (Unlock)
    [HttpPatch("admin/unlock/{id}")]
    [Authorize(Roles = DefaultRoles.Admin, Policy = Permissions.Users_ManageRoles)]
    public async Task<IActionResult> Unlock([FromRoute] string id)
    {
        var result = await _userService.Unlock(id);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }


}
