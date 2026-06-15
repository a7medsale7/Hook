using System;
using FluentValidation;
using Hook.Application.Contracts.Community;
using Hook.Domain.Enums;

namespace Hook.Application.Validators.Community;

public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(2000)
            .WithMessage("Content must not exceed 2000 characters.");

        RuleFor(x => x.Category)
            .IsInEnum()
            .WithMessage("Invalid post category.");

        // Event specific validation
        RuleFor(x => x.EventDate)
            .NotEmpty()
            .When(x => x.Category == PostCategory.Event)
            .WithMessage("Event date is required for event posts.")
            .Must(date => date == null || date.Value > DateTime.UtcNow)
            .When(x => x.Category == PostCategory.Event)
            .WithMessage("Event date must be in the future.");

        RuleFor(x => x.MaxParticipants)
            .NotEmpty()
            .When(x => x.Category == PostCategory.Event)
            .WithMessage("Max participants count is required for event posts.")
            .GreaterThan(0)
            .When(x => x.Category == PostCategory.Event)
            .WithMessage("Max participants must be greater than 0.");
    }
}
