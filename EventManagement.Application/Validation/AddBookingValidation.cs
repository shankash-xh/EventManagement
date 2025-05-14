using EventManagement.Application.Request.Booking;
using EventManagement.Shared.Enums;
using FluentValidation;

namespace EventManagement.Application.Validation;

public class AddBookingValidation : AbstractValidator<AddBookingRequest>
{
    public AddBookingValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.");
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("EventId is required.")
            .GreaterThan(0).WithMessage("EventId must be greater than 0.");
        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future.");
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status is not valid.");
    }
}
