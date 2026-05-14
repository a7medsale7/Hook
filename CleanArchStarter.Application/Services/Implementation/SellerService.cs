using Hangfire;
using Hook.Application.Abstractions.Result;
using Hook.Application.Contracts.Seller;
using Hook.Application.Errors;
using Hook.Application.Services.Interfaces;
using Hook.Domain.Abstractions;
using Hook.Domain.Abstractions.Repositories;
using Hook.Domain.Consts;
using Hook.Domain.Entities;
using Hook.Domain.Enums;
using Hook.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Services.Implementation
{
    public class SellerService : ISellerService
    {
        private readonly ISellerProfileRepository _sellerProfileRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SellerService(
            ISellerProfileRepository sellerProfileRepository,
            IFileService fileService,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _sellerProfileRepository = sellerProfileRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<Result<SellerResponse>> ApplyAsync(string userId, ApplySellerRequest request, CancellationToken cancellationToken = default)
        {
            var hasProfile = await _sellerProfileRepository.HasProfileAsync(userId);
            if (hasProfile)
                return Result.Failure<SellerResponse>(SellerErrors.AlreadyApplied);

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure<SellerResponse>(new Error("Seller.UserNotFound", "User not found."));

            var nationalIdUrl = await _fileService.SaveFileAsync(request.NationalIdImage, "sellers/national-ids");
            string? storeImageUrl = null;
            if (request.StoreImage is not null)
            {
                storeImageUrl = await _fileService.SaveFileAsync(request.StoreImage, "sellers/stores");
            }

            var profile = new SellerProfile
            {
                UserId = userId,
                SellerName = request.SellerName,
                PhoneNumber = request.PhoneNumber,
                Governorate = request.Governorate,
                City = request.City,
                Address = request.Address,
                NationalIdPhotoUrl = nationalIdUrl,
                StoreImageUrl = storeImageUrl,
                Status = RequestStatus.Pending
            };

            await _sellerProfileRepository.AddAsync(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
                user.Email!,
                "Hook - Seller Request Submitted",
                EmailTemplates.GetSellerRequestSubmittedTemplate($"{user.FirstName} {user.LastName}", request.SellerName)
            ));

            var savedProfile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            return Result.Success(ToResponse(savedProfile!));
        }

        public async Task<Result<SellerResponse>> GetProfileAsync(string userId, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (profile is null)
                return Result.Failure<SellerResponse>(SellerErrors.ProfileNotFound);

            return Result.Success(ToResponse(profile));
        }

        public async Task<Result> UpdateProfileAsync(string userId, UpdateSellerProfileRequest request, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByUserIdAsync(userId);
            if (profile is null)
                return Result.Failure(SellerErrors.ProfileNotFound);

            if (profile.Status != RequestStatus.Approved)
                return Result.Failure(new Error("Seller.NotApproved", "Only approved sellers can update their profile."));

            profile.SellerName = request.SellerName;
            profile.PhoneNumber = request.PhoneNumber;
            profile.Governorate = request.Governorate;
            profile.City = request.City;
            profile.Address = request.Address;

            if (request.StoreImage is not null)
            {
                if (!string.IsNullOrEmpty(profile.StoreImageUrl))
                {
                    _fileService.DeleteFile(profile.StoreImageUrl);
                }
                profile.StoreImageUrl = await _fileService.SaveFileAsync(request.StoreImage, "sellers/stores");
            }

            _sellerProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result<IEnumerable<SellerResponse>>> GetPendingApplicationsAsync(CancellationToken cancellationToken = default)
        {
            var applications = await _sellerProfileRepository.GetPendingApplicationsAsync();
            return Result.Success(applications.Select(ToResponse));
        }

        public async Task<Result<IEnumerable<SellerResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var sellers = await _sellerProfileRepository.GetAllAsync();
            return Result.Success(sellers.Select(ToResponse));
        }

        public async Task<Result> UpdateStatusAsync(UpdateSellerStatusRequest request, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByIdAsync(request.ProfileId);
            if (profile is null)
                return Result.Failure(SellerErrors.ProfileNotFound);

            var user = profile.User;
            if (user is null)
                return Result.Failure(new Error("Seller.UserNotFound", "User not found."));

            if (request.IsApproved)
            {
                profile.Status = RequestStatus.Approved;
                profile.AdminRejectionReason = null;

                await _userManager.AddToRoleAsync(user, DefaultRoles.Seller);

                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
                    user.Email!,
                    "Hook - Congratulations! You are now a Seller",
                    EmailTemplates.GetSellerApprovedTemplate($"{user.FirstName} {user.LastName}", profile.SellerName)
                ));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.RejectionReason))
                    return Result.Failure(SellerErrors.RejectionReasonRequired);

                profile.Status = RequestStatus.Rejected;
                profile.AdminRejectionReason = request.RejectionReason;

                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(
                    user.Email!,
                    "Hook - Seller Request Rejected",
                    EmailTemplates.GetSellerRejectedTemplate($"{user.FirstName} {user.LastName}", profile.SellerName, request.RejectionReason)
                ));
            }

            _sellerProfileRepository.Update(profile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByIdAsync(id);
            if (profile is null)
                return Result.Failure(SellerErrors.ProfileNotFound);

            _sellerProfileRepository.SoftDelete(profile);

            var user = profile.User;
            if (user is not null)
            {
                await _userManager.RemoveFromRoleAsync(user, DefaultRoles.Seller);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<SellerResponse>>> GetDeletedAsync(CancellationToken cancellationToken = default)
        {
            var deleted = await _sellerProfileRepository.GetDeletedAsync();
            return Result.Success(deleted.Select(ToResponse));
        }

        public async Task<Result> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var profile = await _sellerProfileRepository.GetByIdWithDeletedAsync(id);
            if (profile is null)
                return Result.Failure(SellerErrors.ProfileNotFound);

            profile.IsDeleted = false;
            _sellerProfileRepository.Update(profile);

            if (profile.Status == RequestStatus.Approved)
            {
                var user = profile.User;
                if (user is not null)
                {
                    await _userManager.AddToRoleAsync(user, DefaultRoles.Seller);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private static SellerResponse ToResponse(SellerProfile profile) => new SellerResponse
        {
            Id = profile.Id,
            UserId = profile.UserId,
            FullName = profile.User is not null ? $"{profile.User.FirstName} {profile.User.LastName}" : "Unknown",
            Email = profile.User?.Email ?? string.Empty,
            SellerName = profile.SellerName,
            PhoneNumber = profile.PhoneNumber,
            Governorate = profile.Governorate,
            City = profile.City,
            Address = profile.Address,
            NationalIdPhotoUrl = profile.NationalIdPhotoUrl,
            StoreImageUrl = profile.StoreImageUrl,
            Status = profile.Status,
            AdminRejectionReason = profile.AdminRejectionReason,
            CreatedOn = profile.CreatedOn
        };
    }


}
