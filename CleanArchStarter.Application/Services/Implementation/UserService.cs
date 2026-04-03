using Hook.Application.Abstractions.Result;
using Hangfire;
using Hook.Application.Contracts.Users;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Consts;
using Hook.Domain.Entities;
using Hook.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRoleService _roleService;
    private readonly IFileService _fileService;
    public UserService(
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender,
        ApplicationDbContext context,
        RoleManager<ApplicationRole> roleManager,
        IRoleService roleService,
        IFileService fileService)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _context = context;
        _roleManager = roleManager;
        _roleService = roleService;
        _fileService = fileService;
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
    await (from u in _context.Users
           join ur in _context.UserRoles
           on u.Id equals ur.UserId
           join r in _context.Roles
           on ur.RoleId equals r.Id into roles
           where !roles.Any(x => x.Name == DefaultRoles.User)
           select new
           {
               u.Id,
               u.FirstName,
               u.LastName,
               u.Email,
               u.IsDisabled,
               u.Governorate,
               Roles = roles.Select(x => x.Name!).ToList()
           }
            )
            .GroupBy(u => new { u.Id, u.FirstName, u.LastName, u.Email, u.IsDisabled , u.Governorate })
           .Select(u => new UserResponse
           {
               Id = u.Key.Id,
               FirstName = u.Key.FirstName,
               LastName = u.Key.LastName,
               Email = u.Key.Email,
               IsDisabled = u.Key.IsDisabled,
               Roles = u.SelectMany(x => x.Roles),
               Governorate = u.Key.Governorate


           })
           .ToListAsync(cancellationToken);

    public async Task<Result<UserResponse>> GetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var roles = await _userManager.GetRolesAsync(user);

        var response = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsDisabled = user.IsDisabled,
            Governorate = user.Governorate!,
            Roles = roles
        };

        return Result.Success(response);
    }

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);
        return Result.Success(new UserProfileResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Governorate = user.Governorate,
            Bio = user.Bio,
            ProfilePictureUrl = user.ProfilePictureUrl
        });
    }
    public async Task<Result<UserProfileResponse>> UpdateProfileAsync(string userId, UpdateProfileRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);

        // --- التعامل مع الملف (رفع الصورة) ---
        if (request.Image is not null)
        {
            // مسح الصورة القديمة إذا وجدت
            _fileService.DeleteFile(user.ProfilePictureUrl);
            
            // حفظ الصورة الجديدة
            user.ProfilePictureUrl = await _fileService.SaveFileAsync(request.Image, "profiles");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Governorate = request.Governorate;
        user.Bio = request.Bio;
        await _userManager.UpdateAsync(user);
        return Result.Success(new UserProfileResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Governorate = user.Governorate,
            Bio = user.Bio,
            ProfilePictureUrl = user.ProfilePictureUrl
        });
    }

    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            // ???? ???? ??? ????? ????? (?? Password too short ?????)
            return Result.Failure(new Error("User.InvalidPassword", result.Errors.First().Description));
        }
        return Result.Success();
    }
    public async Task<Result> ForgotPasswordAsync(ForgotPasswordRequest request, string originUrl)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        // ?? ????? Error ?? ??????? ?? ????? ???? ??? Security (???? ???? ???? ????????? ???????)
        if (user is null)
            return Result.Success();
        // 1. Generate Token
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // 2. Encode token for URL safety
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        // 3. Create reset link (Point to Frontend or API helper)
        var resetLink = $"{originUrl}/reset-password?email={user.Email}&token={encodedToken}";
        
        // 4. Enqueue email sending job
        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
            user.Email!,
            "Hook - Password Reset Request ⚓",
            $"Hello {user.FirstName},<br><br>You requested to reset your password. Please click the link below to proceed:<br><br><a href='{resetLink}' style='display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;'>Reset Password</a><br><br>If you didn't request this, please ignore this email."
        ));
        return Result.Success();
    }
    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);
        try
        {
            // ?? ????? ?????? ???? ??? ?? ??? URL
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);
            // ??????? ???????
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (!result.Succeeded)
                return Result.Failure(UserErrors.InvalidToken);
            return Result.Success();
        }
        catch
        {
            return Result.Failure(UserErrors.InvalidToken);
        }
    }

    public async Task<Result<UserResponse>> AddAsync(CreateUserReqeust request, CancellationToken cancellationToken = default)
    {
        // 1. ?????? ?? ???? ?????? ?????????? ?????? (???????? EmailAlreadyExists ????? ?? DisabledUser)
        var emailExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (emailExists)
            return Result.Failure<UserResponse>(UserErrors.EmailAlreadyExists);

        // 2. ?????? ?? ?? ??????? (Roles) ??????? ?????? ????? ?? ??????
        // ???????? _roleManager ??? ?????? ??????
        var systemRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync(cancellationToken);

        if (request.Roles.Except(systemRoles).Any())
            return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

        // 3. ????? DTO ??? ApplicationUser
        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email, // ????????? ?? ??? ???????? ?? ??????
            EmailConfirmed = true,   // ????? ?? ??????? ?? ?? ????? ????? ?????? ????????
            Governorate = request.Governorate
        };

        // 4. ????? ???????? ?? ???? ??????
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<UserResponse>(new Error(error.Code, error.Description));
        }

        // 5. ????? ???????? ??? ??????? ????????
        if (request.Roles.Any())
        {
            var rolesResult = await _userManager.AddToRolesAsync(user, request.Roles);
            if (!rolesResult.Succeeded)
            {
                var error = rolesResult.Errors.First();
                return Result.Failure<UserResponse>(new Error(error.Code, error.Description));
            }
        }

        // 6. ????? ??? Response ???????
        var response = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsDisabled = user.IsDisabled,
            Roles = request.Roles,
            Governorate = user.Governorate!
        };

        return Result.Success(response);
    }



    // --- ????? ??????? (Update) ---
    public async Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        // 1. ?????? ?? ?? ?????? ?????????? ??? ?????? ?? ??? ??? ???
        var emailExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id, cancellationToken);
        if (emailExists)
            return Result.Failure(UserErrors.EmailAlreadyExists);

        // 2. ??? ???????? ??????? ?? ?????
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        // 3. ?????? ?? ?? ??????? ??????? ????? ??????? ?? ??????
        var systemRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync(cancellationToken);
        if (request.Roles.Except(systemRoles).Any())
            return Result.Failure(UserErrors.InvalidRoles);

        // 4. ????? ???????? (?????? ?? ???????? Adapt)
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.Governorate = request.Governorate;

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            // 5. ????? ??????? (??? ??????? ??????? ?????? ???????)
            // ??????? ??????? ?? Identity (???? ??????):
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRolesAsync(user, request.Roles);

            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description));
    }

    // --- ????? ????? ?????? (?????/?????) ---
    public async Task<Result> ToggleStatus(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        user.IsDisabled = !user.IsDisabled;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded
            ? Result.Success(result)
            : Result.Failure(new Error(result.Errors.First().Code, result.Errors.First().Description));
    }

    // --- ????? ?? ????? (Unlock) ---
    public async Task<Result> Unlock(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        // ????? ????? ?????? ????? ??? ????? ?????
        var result = await _userManager.SetLockoutEndDateAsync(user, null);

        return result.Succeeded
            ? Result.Success()
            : Result.Failure(new Error(result.Errors.First().Code, result.Errors.First().Description));
    }

}