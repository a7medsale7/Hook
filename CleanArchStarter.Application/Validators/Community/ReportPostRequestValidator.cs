using FluentValidation;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Validators.Community;

public class ReportPostRequestValidator : AbstractValidator<ReportPostRequest>
{
    public ReportPostRequestValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("Reason for report is required.")
            .MaximumLength(500)
            .WithMessage("Reason must not exceed 500 characters.");
    }
}
