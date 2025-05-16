using EventManagement.Application.Request.User;
using FluentValidation;

namespace EventManagement.Application.Validation;

public class UserRequestValidation : AbstractValidator<LoginUserRequest>
{
    public UserRequestValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
