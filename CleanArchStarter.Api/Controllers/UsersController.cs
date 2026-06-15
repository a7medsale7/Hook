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
        var result = await _userService.GetProfileAsync(userId, userId);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("allroles/profile/{userId}")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetProfileById([FromRoute] string userId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();
        var result = await _userService.GetProfileAsync(userId, currentUserId);
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

    [HttpGet("admin/GetAll")]
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

    [HttpGet("allroles/reset-password")]
    [AllowAnonymous]
    public IActionResult ResetPassword([FromQuery] string email, [FromQuery] string token)
    {
        string title = "Reset Password | Hook";
        string icon = "⚓";
        string color = "#38bdf8";

        string html = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{title}</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #0f172a 0%, #1e293b 100%);
            color: #f8fafc;
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            margin: 0;
        }}
        .container {{
            text-align: center;
            background: rgba(30, 41, 59, 0.7);
            backdrop-filter: blur(10px);
            padding: 2.5rem;
            border-radius: 1.5rem;
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
            max-width: 400px;
            width: 90%;
            border: 1px solid rgba(255, 255, 255, 0.1);
            animation: fadeIn 0.8s ease-out;
        }}
        @keyframes fadeIn {{
            from {{ opacity: 0; transform: translateY(20px); }}
            to {{ opacity: 1; transform: translateY(0); }}
        }}
        .icon {{
            font-size: 3.5rem;
            margin-bottom: 1rem;
            color: {color};
        }}
        h1 {{
            margin: 0 0 0.5rem;
            font-size: 1.75rem;
            color: #fff;
        }}
        p {{
            color: #94a3b8;
            font-size: 0.9rem;
            margin-bottom: 2rem;
        }}
        .form-group {{
            text-align: left;
            margin-bottom: 1.25rem;
        }}
        label {{
            display: block;
            margin-bottom: 0.5rem;
            font-size: 0.85rem;
            color: #94a3b8;
        }}
        input {{
            width: 100%;
            padding: 0.75rem 1rem;
            background: rgba(15, 23, 42, 0.6);
            border: 1px solid rgba(255, 255, 255, 0.1);
            border-radius: 0.75rem;
            color: #fff;
            font-size: 1rem;
            box-sizing: border-box;
            transition: all 0.3s ease;
        }}
        input:focus {{
            outline: none;
            border-color: {color};
            box-shadow: 0 0 0 2px rgba(56, 189, 248, 0.2);
        }}
        input[readonly] {{
            background: rgba(15, 23, 42, 0.3);
            color: #64748b;
            cursor: not-allowed;
        }}
        .btn {{
            width: 100%;
            background: {color};
            color: #0f172a;
            padding: 0.85rem;
            border: none;
            border-radius: 0.75rem;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 1rem;
        }}
        .btn:hover {{
            opacity: 0.9;
            transform: translateY(-2px);
        }}
        .btn:disabled {{
            opacity: 0.5;
            cursor: not-allowed;
        }}
        #message {{
            margin-top: 1.5rem;
            font-size: 0.9rem;
            border-radius: 0.75rem;
            padding: 0.75rem;
            display: none;
        }}
        .success {{
            background: rgba(34, 197, 94, 0.1);
            color: #4ade80;
            border: 1px solid rgba(34, 197, 94, 0.2);
        }}
        .error {{
            background: rgba(239, 68, 68, 0.1);
            color: #f87171;
            border: 1px solid rgba(239, 68, 68, 0.2);
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""icon"">{icon}</div>
        <h1>Reset Password</h1>
        <p>Enter your new password below to secure your account.</p>
        
        <form id=""resetForm"">
            <input type=""hidden"" id=""email"" value=""{email}"">
            <input type=""hidden"" id=""token"" value=""{token}"">
            
            <div class=""form-group"">
                <label>Email Address</label>
                <input type=""text"" value=""{email}"" readonly>
            </div>
            
            <div class=""form-group"">
                <label>New Password</label>
                <input type=""password"" id=""newPassword"" placeholder=""••••••••"" required minlength=""6"">
            </div>
            
            <div class=""form-group"">
                <label>Confirm Password</label>
                <input type=""password"" id=""confirmPassword"" placeholder=""••••••••"" required minlength=""6"">
            </div>
            
            <button type=""submit"" id=""submitBtn"" class=""btn"">Update Password</button>
        </form>
        
        <div id=""message""></div>
    </div>

    <script>
        const resetForm = document.getElementById('resetForm');
        const submitBtn = document.getElementById('submitBtn');
        const messageDiv = document.getElementById('message');

        resetForm.addEventListener('submit', async (e) => {{
            e.preventDefault();
            
            const email = document.getElementById('email').value;
            const token = document.getElementById('token').value;
            const newPassword = document.getElementById('newPassword').value;
            const confirmPassword = document.getElementById('confirmPassword').value;

            if (newPassword !== confirmPassword) {{
                showMessage('Passwords do not match!', 'error');
                return;
            }}

            submitBtn.disabled = true;
            submitBtn.textContent = 'Updating...';
            messageDiv.style.display = 'none';

            try {{
                const response = await fetch('/api/Users/allroles/reset-password', {{
                    method: 'POST',
                    headers: {{
                        'Content-Type': 'application/json'
                    }},
                    body: JSON.stringify({{
                        email: email,
                        token: token,
                        newPassword: newPassword
                    }})
                }});

                if (response.ok) {{
                    showMessage('Password updated successfully! You can now log in.', 'success');
                    resetForm.style.display = 'none';
                }} else {{
                    const error = await response.json();
                    showMessage(error.description || 'Failed to reset password. The link might be expired.', 'error');
                }}
            }} catch (err) {{
                showMessage('An error occurred. Please try again later.', 'error');
            }} finally {{
                submitBtn.disabled = false;
                submitBtn.textContent = 'Update Password';
            }}
        }});

        function showMessage(msg, type) {{
            messageDiv.textContent = msg;
            messageDiv.className = type;
            messageDiv.style.display = 'block';
        }}
    </script>
</body>
</html>";

        return new ContentResult
        {
            Content = html,
            ContentType = "text/html",
            StatusCode = 200
        };
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


    [HttpPost("admin /AddNewUser")]
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

    [HttpGet("{userId}/followers")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetFollowers([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();

        var result = await _userService.GetFollowersAsync(userId, currentUserId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{userId}/following")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetFollowing([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null) return Unauthorized();

        var result = await _userService.GetFollowingAsync(userId, currentUserId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my-followers")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetMyFollowers(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _userService.GetFollowersAsync(userId, userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("my-following")]
    [Authorize(Policy = Permissions.Users_ViewProfile)]
    public async Task<IActionResult> GetMyFollowing(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _userService.GetFollowingAsync(userId, userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}
