using System;
using FluentValidation;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Validators.Community;

public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
{
    public UpdatePostRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(2000)
            .WithMessage("Content must not exceed 2000 characters.");

        RuleFor(x => x.EventDate)
            .Must(date => date == null || date.Value > DateTime.UtcNow)
            .WithMessage("Event date must be in the future.")
            .When(x => x.EventDate.HasValue);

        RuleFor(x => x.MaxParticipants)
            .GreaterThan(0)
            .WithMessage("Max participants must be greater than 0.")
            .When(x => x.MaxParticipants.HasValue);
    }
}
