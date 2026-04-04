using FluentValidation;
using Hook.Application.Contracts.Users;

namespace Hook.Application.Validators.User;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[0-9]{10,15}$")
            .WithMessage("Invalid phone number format.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.Image)
            .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
            .WithMessage("Image size cannot exceed 2MB.")
            .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png" }.Contains(System.IO.Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Only .jpg, .jpeg, and .png images are allowed.");
    }
}
