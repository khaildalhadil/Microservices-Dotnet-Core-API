using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

// Rules for the register payload. Runs before the service touches the DB.
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.PersonName)
            .NotEmpty().WithMessage("Person name is required.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Gender is not valid.");
    }
}
