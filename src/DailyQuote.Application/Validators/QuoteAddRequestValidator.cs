using DailyQuote.Application.DTO;
using DailyQuote.Domain.Entities;
using FluentValidation;

namespace DailyQuote.Application.Validators
{
    public class QuoteAddRequestValidator : AbstractValidator<QuoteAddRequest>
    {
        public QuoteAddRequestValidator()
        {
            RuleFor(quote => quote.Content).NotEmpty()
                .MaximumLength(100);

            RuleFor(quote => quote.Type).NotEmpty();

            When(quote => !string.IsNullOrWhiteSpace(quote.Type), () =>
            {
                RuleFor(quote => quote.Type)
                    .Must(i => Enum.IsDefined(typeof(QuoteType), i));
            });
        }
    }
}
