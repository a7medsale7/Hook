using FluentValidation;
using Hook.Application.Contracts.Marketplace.Products;
using Hook.Application.Validators.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Application.Validators.Marketplace.Products
{
    public class CreateMarketplaceProductRequestValidator : AbstractValidator<CreateMarketplaceProductRequest>
    {
        public CreateMarketplaceProductRequestValidator() 
        { 
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Product name is required")
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be 0 or more");

            RuleFor(x => x.Images)
                .NotNull().WithMessage("Images are required")
                .Must(x => x.Count > 0).WithMessage("At least one image is required");

            RuleForEach(x => x.Images)
            .SetValidator(new FileValidator());

        }
    }
}
