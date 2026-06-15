using FluentValidation;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Validators.Community;

public class ResolveComplaintRequestValidator : AbstractValidator<ResolveComplaintRequest>
{
    public ResolveComplaintRequestValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status for complaint resolution.");

        RuleFor(x => x.AdminNotes)
            .MaximumLength(1000)
            .WithMessage("Admin notes must not exceed 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.AdminNotes));
    }
}
