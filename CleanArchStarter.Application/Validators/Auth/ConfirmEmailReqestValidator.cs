using CleanArchStarter.Application.Contracts.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchStarter.Application.Validators.Auth;
public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailReqest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Confirmation code is required.")
            .MinimumLength(10)
            .WithMessage("Invalid confirmation code.");
    }
}