using FluentValidation;
using Hook.Application.Contracts.Booking;

namespace Hook.Application.Validators.Booking;

public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.TripDateId)
            .NotEmpty().WithMessage("Trip date is required.");

        RuleFor(x => x.NumberOfParticipants)
            .GreaterThan(0).WithMessage("Number of participants must be at least 1.")
            .LessThanOrEqualTo(100).WithMessage("Number of participants cannot exceed 100 per booking.");

        RuleFor(x => x.SpecialRequests)
            .MaximumLength(500).WithMessage("Special requests cannot exceed 500 characters.");
    }
}
