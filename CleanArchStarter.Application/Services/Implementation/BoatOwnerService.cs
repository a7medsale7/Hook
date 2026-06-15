using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.BoatOwner;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Domain.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation;

public class BoatOwnerService : IBoatOwnerService
{
    private readonly IBoatOwnerRepository _boatOwnerRepository;
    private readonly IFileService _fileService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public BoatOwnerService(
        IBoatOwnerRepository boatOwnerRepository,
        IFileService fileService,
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender)
    {
        _boatOwnerRepository = boatOwnerRepository;
        _fileService = fileService;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<Result<BoatOwnerResponse>> ApplyAsync(string userId, ApplyBoatOwnerRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Check if already applied
        var hasProfile = await _boatOwnerRepository.HasProfileAsync(userId);
        if (hasProfile)
            return Result.Failure<BoatOwnerResponse>(BoatOwnerErrors.AlreadyApplied);

        // 2. Upload Documents
        string nationalIdUrl = await _fileService.SaveFileAsync(request.NationalIdImage, "boat-owners/national-ids");
        string licenseUrl = await _fileService.SaveFileAsync(request.BoatLicenseImage, "boat-owners/licenses");

        // 3. Create Profile
        var profile = new BoatOwnerProfile
        {
            UserId = userId,
            NationalIdNumber = request.NationalIdNumber,
            NationalIdPhotoUrl = nationalIdUrl,
            BoatLicenseNumber = request.BoatLicenseNumber,
            BoatLicensePhotoUrl = licenseUrl,
            InstaPayNumber = request.InstaPayNumber,
            VodafoneCashNumber = request.VodafoneCashNumber,
            Status = RequestStatus.Pending
        };

        await _boatOwnerRepository.AddAsync(profile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 4. Return Response
        // Refresh to get User data
        var savedProfile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        return Result.Success(ToResponse(savedProfile!));
    }

    public async Task<Result<BoatOwnerResponse>> GetProfileAsync(string userId, CancellationToken cancellationToken = default)
    {
        var profile = await _boatOwnerRepository.GetByUserIdAsync(userId);
        if (profile is null)
            return Result.Failure<BoatOwnerResponse>(BoatOwnerErrors.ProfileNotFound);

        return Result.Success(ToResponse(profile));
    }

    public async Task<Result<IEnumerable<BoatOwnerResponse>>> GetPendingApplicationsAsync(CancellationToken cancellationToken = default)
    {
        var applications = await _boatOwnerRepository.GetPendingApplicationsAsync();
        var response = applications.Select(ToResponse);
        return Result.Success(response);
    }

    public async Task<Result<IEnumerable<BoatOwnerResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var boatOwners = await _boatOwnerRepository.GetAllAsync();
        var response = boatOwners.Select(ToResponse);
        return Result.Success(response);
    }

    public async Task<Result> UpdateStatusAsync(UpdateBoatOwnerStatusRequest request, CancellationToken cancellationToken = default)
    {
        var profile = await _boatOwnerRepository.GetByIdAsync(request.ProfileId);
        if (profile is null)
            return Result.Failure(BoatOwnerErrors.ProfileNotFound);

        if (request.IsApproved)
        {
            profile.Status = RequestStatus.Approved;
            profile.AdminRejectionReason = null;

            // Add BoatOwner role to user
            var user = profile.User;
            if (user is not null)
            {
                await _userManager.AddToRoleAsync(user, DefaultRoles.BoatOwner);
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(request.RejectionReason))
                return Result.Failure(BoatOwnerErrors.RejectionReasonRequired);

            profile.Status = RequestStatus.Rejected;
            profile.AdminRejectionReason = request.RejectionReason;
        }

        _boatOwnerRepository.Update(profile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send Email Notification
        var userToEmail = profile.User;
        if (userToEmail is not null && !string.IsNullOrWhiteSpace(userToEmail.Email))
        {
            string subject = request.IsApproved ? "Hook: Boat Owner Application Approved" : "Hook: Boat Owner Application Rejected";
            string message = request.IsApproved
                ? $"Dear {userToEmail.FirstName},<br/><br/>Congratulations! Your application to become a Boat Owner has been approved. You can now start adding your boats and trips."
                : $"Dear {userToEmail.FirstName},<br/><br/>We regret to inform you that your application to become a Boat Owner has been rejected.<br/><br/>Reason: {request.RejectionReason}";

            try
            {
                await _emailSender.SendEmailAsync(userToEmail.Email, subject, message);
            }
            catch
            {
                // Ignore email failure so the application status update still succeeds
            }
        }

        return Result.Success();
    }

    public async Task<Result> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await _boatOwnerRepository.GetByIdAsync(id);
        if (profile is null)
            return Result.Failure(BoatOwnerErrors.ProfileNotFound);

        _boatOwnerRepository.SoftDelete(profile);

        // Remove BoatOwner role from user if they have it
        var user = profile.User;
        if (user is not null)
        {
            await _userManager.RemoveFromRoleAsync(user, DefaultRoles.BoatOwner);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<BoatOwnerResponse>>> GetDeletedAsync(CancellationToken cancellationToken = default)
    {
        var deletedProfiles = await _boatOwnerRepository.GetDeletedAsync();
        var response = deletedProfiles.Select(ToResponse);
        return Result.Success(response);
    }

    public async Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await _boatOwnerRepository.GetByIdWithDeletedAsync(id);
        if (profile is null)
            return Result.Failure(BoatOwnerErrors.ProfileNotFound);

        profile.IsDeleted = false;
        _boatOwnerRepository.Update(profile);

        // If it was already approved, re-add the role
        if (profile.Status == RequestStatus.Approved)
        {
            var user = profile.User;
            if (user is not null)
            {
                await _userManager.AddToRoleAsync(user, DefaultRoles.BoatOwner);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private static BoatOwnerResponse ToResponse(BoatOwnerProfile profile) => new BoatOwnerResponse
    {
        Id = profile.Id,
        UserId = profile.UserId,
        FullName = profile.User is not null ? $"{profile.User.FirstName} {profile.User.LastName}" : "Unknown",
        Email = profile.User?.Email ?? string.Empty,
        NationalIdNumber = profile.NationalIdNumber,
        NationalIdPhotoUrl = profile.NationalIdPhotoUrl,
        BoatLicenseNumber = profile.BoatLicenseNumber,
        BoatLicensePhotoUrl = profile.BoatLicensePhotoUrl,
        InstaPayNumber = profile.InstaPayNumber,
        VodafoneCashNumber = profile.VodafoneCashNumber,
        Status = profile.Status,
        AdminRejectionReason = profile.AdminRejectionReason,
        CreatedOn = profile.CreatedOn
    };
}
