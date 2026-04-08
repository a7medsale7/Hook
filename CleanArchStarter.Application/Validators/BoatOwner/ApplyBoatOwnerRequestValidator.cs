using FluentValidation;
using Hook.Application.Contracts.BoatOwner;
using Hook.Application.Validators.File;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Validators.BoatOwner;
public class ApplyBoatOwnerRequestValidator : AbstractValidator<ApplyBoatOwnerRequest>
{
    public ApplyBoatOwnerRequestValidator()
    {
        // 1. National ID Validation
        RuleFor(x => x.NationalIdNumber)
            .NotEmpty().WithMessage("National ID number is required.")
            .Length(14).WithMessage("National ID must be exactly 14 characters.");
        RuleFor(x => x.NationalIdImage)
            .NotNull().WithMessage("National ID photo is required.")
            .SetValidator(new FileValidator());
        // 2. Boat License Validation
        RuleFor(x => x.BoatLicenseNumber)
            .NotEmpty().WithMessage("Boat license number is required.")
            .MaximumLength(50).WithMessage("License number cannot exceed 50 characters.");
        RuleFor(x => x.BoatLicenseImage)
            .NotNull().WithMessage("Boat license photo is required.")
            .SetValidator(new FileValidator());
        // 3. Payment Info Validation (at least one is required)
        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.InstaPayNumber) || !string.IsNullOrEmpty(x.VodafoneCashNumber))
            .WithMessage("At least one payment method is required (InstaPay number or Vodafone Cash number).");
    }
}
