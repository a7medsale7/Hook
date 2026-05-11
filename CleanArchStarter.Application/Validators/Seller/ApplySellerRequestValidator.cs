using FluentValidation;
using Hook.Application.Contracts.Seller;
using Hook.Application.Validators.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Validators.Seller
{
    public class ApplySellerRequestValidator : AbstractValidator<ApplySellerRequest>
    {
        public ApplySellerRequestValidator() 
        {
            RuleFor(x => x.SellerName)
            .NotEmpty().WithMessage("Seller name is required.")
            .MaximumLength(150).WithMessage("Seller name cannot exceed 150 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MaximumLength(50).WithMessage("Phone number cannot exceed 50 characters.");

            RuleFor(x => x.Governorate)
                .NotEmpty().WithMessage("Governorate is required.")
                .MaximumLength(100).WithMessage("Governorate cannot exceed 100 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(400).WithMessage("Address cannot exceed 400 characters.");

            RuleFor(x => x.NationalIdImage)
                .NotNull().WithMessage("National ID photo is required.")
                .SetValidator(new FileValidator());
        }
    }
}
