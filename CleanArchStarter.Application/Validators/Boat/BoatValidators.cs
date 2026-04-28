using FluentValidation;
using Hook.Application.Contracts.Boat;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace Hook.Application.Validators.Boat;

public class CreateBoatRequestValidator : AbstractValidator<CreateBoatRequest>
{
    public CreateBoatRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Boat name is required.")
            .MaximumLength(100).WithMessage("Boat name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(20).WithMessage("Description should be at least 20 characters.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be at least 1 person.");

        RuleForEach(x => x.Images).SetValidator(new BoatImageValidator());
    }
}

public class UpdateBoatRequestValidator : AbstractValidator<UpdateBoatRequest>
{
    public UpdateBoatRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Boat name is required.")
            .MaximumLength(100).WithMessage("Boat name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be at least 1 person.");

    }
}

public class BoatImageValidator : AbstractValidator<IFormFile>
{
    public BoatImageValidator()
    {
        RuleFor(x => x.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024).WithMessage("Each image must be less than 5MB.");

        RuleFor(x => x.FileName)
            .Must(HaveValidExtension).WithMessage("Invalid image format. Allowed: .jpg, .jpeg, .png");
    }

    private bool HaveValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        string[] validExtensions = { ".jpg", ".jpeg", ".png" };
        return validExtensions.Contains(extension);
    }
}
