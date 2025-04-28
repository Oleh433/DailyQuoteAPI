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
                Id = quote.Id,
                Content = quote.Content,
                Type = quote.Type.ToString()
            };
        }
    }
}
