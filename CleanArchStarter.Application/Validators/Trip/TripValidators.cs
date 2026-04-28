using FluentValidation;
using Hook.Application.Contracts.Trip;
using Hook.Application.Validators.File;
using System;

namespace Hook.Application.Validators.Trip;

public class CreateTripRequestValidator : AbstractValidator<CreateTripRequest>
{
    public CreateTripRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Trip title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

        RuleFor(x => x.DetailedDescription)
            .NotEmpty().WithMessage("Detailed description is required.");

        RuleFor(x => x.LocationName)
            .NotEmpty().WithMessage("Location name is required.");

        RuleFor(x => x.PricePerPerson)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.MaxParticipants)
            .GreaterThan(0).WithMessage("Max participants must be at least one.");

        RuleFor(x => x.BoatId)
            .NotEmpty().WithMessage("Boat identification is required.");

        RuleFor(x => x.Images)
            .NotEmpty().WithMessage("At least one image of the trip is required.");

        RuleForEach(x => x.Images)
            .SetValidator(new FileValidator());
    }
}

public class TripDateRequestValidator : AbstractValidator<TripDateRequest>
{
    public TripDateRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.AvailableSeats)
            .GreaterThan(0).WithMessage("Available seats must be at least one.");
    }
}
