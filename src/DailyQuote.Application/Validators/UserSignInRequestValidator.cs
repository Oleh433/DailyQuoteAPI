using DailyQuote.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.Validators
{
    public class UserSignInRequestValidator : AbstractValidator<UserSignInRequest>
    {
        public UserSignInRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email should be valid email address")
                .NotEmpty().WithMessage("Email should be empty");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password should not be empty")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$").WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, at least one number, and at least one special character.");
        }
    }
}
