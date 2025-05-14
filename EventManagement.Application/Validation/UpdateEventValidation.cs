using EventManagement.Application.Request.Event;
using FluentValidation;

namespace EventManagement.Application.Validation;

public class UpdateEventValidation : AbstractValidator<UpdateEventRequest>
{
    public UpdateEventValidation()
    {
        RuleFor(x=>x.Id)
            .NotEmpty().WithMessage("Event ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Event name is required.")
            .Length(3, 100).WithMessage("Event name must be between 3 and 100 characters.");
        RuleFor(x => x.OrganizerId)
            .NotEmpty().WithMessage("Organizer ID is required.");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .Length(3, 200).WithMessage("Location must be between 3 and 200 characters.");
        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Event time is required.")
            .GreaterThan(DateTime.Now).WithMessage("Event time must be in the future.");
        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
        RuleFor(x => x.IsPrivate)
            .NotNull().WithMessage("IsPrivate field is required.");
    }
}
