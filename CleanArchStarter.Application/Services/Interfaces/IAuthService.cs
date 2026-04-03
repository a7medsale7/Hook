using CleanArchStarter.Application.Abstractions.Result;
using CleanArchStarter.Application.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Application.Services.Interfaces;
public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken);

    Task<Result> RegisterAsync(
        RegisterRequest registerRequest,
        CancellationToken cancellationToken);

    Task<Result> ConfirmEmailAsync(
        ConfirmEmailReqest request,
        CancellationToken cancellationToken);

    Task<Result> ResendConfirmationEmailAsync(
        ResendConfirmationEmailReqest resendConfirmation,
        CancellationToken cancellationToken);

    Task<Result<AuthResponse>> RefreshTokenAsync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken);

    Task<Result> RevokeRefreshTokenAsync(
        string token,
        string refreshToken,
        CancellationToken cancellationToken);
}