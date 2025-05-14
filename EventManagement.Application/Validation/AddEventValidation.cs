using EventManagement.Application.Request.Event;
using FluentValidation;

namespace EventManagement.Application.Validation;

public class AddEventValidation : AbstractValidator<AddEventRequest>
{
    public AddEventValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(x => x.OrganizerId)
            .NotEmpty().WithMessage("OrganizerId is required.")
            .Length(3, 50).WithMessage("OrganizerId must be between 3 and 50 characters.");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .Length(3, 100).WithMessage("Location must be between 3 and 100 characters.");
        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Time is required.")
            .GreaterThan(DateTime.Now).WithMessage("Time must be in the future.");
        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Capacity is required.")
            .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
        RuleFor(x => x.IsPrivate)
            .NotNull().WithMessage("IsPrivate is required.");
    }
}
