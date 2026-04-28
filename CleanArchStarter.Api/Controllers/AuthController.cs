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
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] ConfirmEmailReqest request,
        CancellationToken cancellationToken)
    {
        var result = await authService.ConfirmEmailAsync(
            request,
            cancellationToken);

        string title = result.IsSuccess ? "Email Confirmed! | Hook" : "Confirmation Failed | Hook";
        string icon = result.IsSuccess ? "⚓" : "❌";
        string heading = result.IsSuccess ? "Email Confirmed!" : "Confirmation Failed";
        string message = result.IsSuccess 
            ? "Your account is now ready. You can start exploring trips and booking your next adventure."
            : $"We couldn't confirm your email. {result.Error.Description}";
        string buttonText = "Go to Home";
        string color = result.IsSuccess ? "#38bdf8" : "#f43f5e";

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
            height: 100vh;
            margin: 0;
            overflow: hidden;
        }}
        .container {{
            text-align: center;
            background: rgba(30, 41, 59, 0.7);
            backdrop-filter: blur(10px);
            padding: 3rem;
            border-radius: 1.5rem;
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
            max-width: 450px;
            width: 90%;
            border: 1px solid rgba(255, 255, 255, 0.1);
            animation: fadeIn 0.8s ease-out;
        }}
        @keyframes fadeIn {{
            from {{ opacity: 0; transform: translateY(20px); }}
            to {{ opacity: 1; transform: translateY(0); }}
        }}
        .icon {{
            font-size: 4rem;
            margin-bottom: 1.5rem;
            color: {color};
            animation: scaleIn 0.5s cubic-bezier(0.175, 0.885, 0.32, 1.275);
        }}
        @keyframes scaleIn {{
            from {{ transform: scale(0); }}
            to {{ transform: scale(1); }}
        }}
        h1 {{
            margin: 0 0 1rem;
            font-size: 2rem;
            color: #fff;
        }}
        p {{
            color: #94a3b8;
            line-height: 1.6;
            margin-bottom: 2rem;
        }}
        .btn {{
            display: inline-block;
            background: {color};
            color: #0f172a;
            padding: 0.75rem 2rem;
            border-radius: 9999px;
            text-decoration: none;
            font-weight: 600;
            transition: all 0.3s ease;
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.2);
        }}
        .btn:hover {{
            opacity: 0.9;
            transform: translateY(-2px);
            box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.3);
        }}
        .loader {{
            margin-top: 1.5rem;
            font-size: 0.875rem;
            color: #64748b;
        }}
    </style>
    <meta http-equiv=""refresh"" content=""5;url=/"">
</head>
<body>
    <div class=""container"">
        <div class=""icon"">{icon}</div>
        <h1>{heading}</h1>
        <p>{message}</p>
        <a href=""/"" class=""btn"">{buttonText}</a>
        <div class=""loader"">Redirecting in <span id=""seconds"">5</span>s...</div>
    </div>
    <script>
        let timeLeft = 5;
        const timer = setInterval(() => {{
            timeLeft--;
            document.getElementById('seconds').textContent = timeLeft;
            if (timeLeft <= 0) clearInterval(timer);
        }}, 1000);
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