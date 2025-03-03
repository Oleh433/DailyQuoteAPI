using DailyQuote.Domain.Entities;

namespace DailyQuote.Application.DTO
{
    public class QuoteResponse
    {
        public Guid Id { get; set; }

        public string? Content { get; set; }

        public string? Type { get; set; }
    }

    public static class QuoteExtensions
    {
        public static QuoteResponse ToQuoteResponse(this Quote quote)
        {
            return new QuoteResponse
            {
                Id = quote.QuoteId,
                Content = quote.QuoteContent,
                Type = quote.QuoteType.ToString()
            };
        }
    }
}
