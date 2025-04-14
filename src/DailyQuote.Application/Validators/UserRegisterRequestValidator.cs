using DailyQuote.Application.DTO;
using DailyQuote.Domain.Enums;
using FluentValidation;

namespace DailyQuote.Application.Validators
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email should be valid email address")
                .NotEmpty().WithMessage("Email should not be empty");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password should not be empty")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, at least one number, and at least one special character.");
        }
    }
}
