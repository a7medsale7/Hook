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
    public class UpdateMarketplaceProductRequestValidator : AbstractValidator<UpdateMarketplaceProductRequest>
    {
        public UpdateMarketplaceProductRequestValidator()
        {
            RuleFor(x => x.ProductId)
           .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be 0 or more.");

            When(x => x.NewImages is not null && x.NewImages.Count > 0, () =>
            {
                RuleForEach(x => x.NewImages!)
                    .SetValidator(new FileValidator());
            });
        }
    }
}
