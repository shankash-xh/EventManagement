using EventManagement.Application.Request.Booking;
using FluentValidation;

namespace EventManagement.Application.Validation;

public class UpdateBookingValidation : AbstractValidator<UpdateBookingRequest>
{
    public UpdateBookingValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("EventId is required.")
            .GreaterThan(0).WithMessage("EventId must be greater than 0.");
        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("CreatedAt cannot be in the future.");
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");
    }
}
