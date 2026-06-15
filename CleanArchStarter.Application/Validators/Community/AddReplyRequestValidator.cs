using FluentValidation;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Validators.Community;

public class AddReplyRequestValidator : AbstractValidator<AddReplyRequest>
{
    public AddReplyRequestValidator()
    {
        RuleFor(x => x.CommentText)
            .NotEmpty()
            .WithMessage("Comment text is required.")
            .MaximumLength(1000)
            .WithMessage("Comment must not exceed 1000 characters.");
    }
}
