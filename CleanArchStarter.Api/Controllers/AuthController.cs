using Hook.Application.Contracts.Auth;
using Hook.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hook.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService authService = authService;

    [HttpPost("allroles/login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.GetTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
            return Unauthorized(new
            {
                result.Error.Code,
                result.Error.Description
            });

        return Ok(result.Value);
    }

    [HttpPost("allroles/refresh")]
    public async Task<IActionResult> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.RefreshTokenAsync(
            request.Token,
            request.RefreshToken,
            cancellationToken);

        if (result.IsFailure)
            return Unauthorized(new
            {
                result.Error.Code,
                result.Error.Description
            });

        return Ok(result.Value);
    }

    [HttpPost("allroles/revoke")]
    public async Task<IActionResult> RevokeToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.RevokeRefreshTokenAsync(
            request.Token,
            request.RefreshToken,
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                result.Error.Code,
                result.Error.Description
            });

        return Ok(new
        {
            message = "Token revoked successfully."
        });
    }

    [HttpPost("allroles/register")]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(
            request,
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                result.Error.Code,
                result.Error.Description
            });

        return Ok(new
        {
            message = "Registration successful. Please check your email to confirm your account."
        });
    }

    [HttpPost("allroles/confirm-email")]
    public async Task<IActionResult> ConfirmEmailPost(
        [FromBody] ConfirmEmailReqest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.ConfirmEmailAsync(
            request,
            cancellationToken);

        return result.IsSuccess 
            ? Ok(new { message = "Email confirmed successfully!" })
            : BadRequest(new { error = result.Error.Description });
    }

    [HttpGet("allroles/confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] ConfirmEmailReqest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.ConfirmEmailAsync(
            request,
            cancellationToken);

        // Redirect to index.html with success/failure query param
        string redirectUrl = result.IsSuccess 
            ? "https://hook.runasp.net/index.html?confirmed=true"
            : "https://hook.runasp.net/index.html?confirmed=false";

return Redirect(redirectUrl);
    }

[HttpPost("allroles/resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail(
        ResendConfirmationEmailReqest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.ResendConfirmationEmailAsync(
            request,
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(new
            {
                result.Error.Code,
                result.Error.Description
            });

        return Ok(new
        {
            message = "If an account with that email exists, a confirmation email has been resent."
        });
    }
}