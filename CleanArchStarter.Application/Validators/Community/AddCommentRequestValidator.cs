using FluentValidation;
using Hook.Application.Contracts.Community;

namespace Hook.Application.Validators.Community;

public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentRequestValidator()
    {
        RuleFor(x => x.CommentText)
            .NotEmpty()
            .WithMessage("Comment text is required.")
            .MaximumLength(1000)
            .WithMessage("Comment must not exceed 1000 characters.");
    }
}
