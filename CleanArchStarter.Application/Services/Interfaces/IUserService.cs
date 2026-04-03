using CleanArchStarter.Application.Abstractions.Result;
using CleanArchStarter.Application.Contracts.Users;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Application.Services.Interfaces;
public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<UserResponse>> GetAsync(string id);
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
    Task<Result<UserProfileResponse>> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    Task<Result> ForgotPasswordAsync(Contracts.Users.ForgotPasswordRequest request, string originUrl);
    Task<Result> ResetPasswordAsync(Contracts.Users.ResetPasswordRequest request);

    Task<Result<UserResponse>> AddAsync(CreateUserReqeust request, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleStatus(string id);
    Task<Result> Unlock(string id);
}
