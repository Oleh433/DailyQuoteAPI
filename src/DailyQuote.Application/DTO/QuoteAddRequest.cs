using DailyQuote.Domain.Entities;

namespace DailyQuote.Application.DTO
{
    public class QuoteAddRequest
    {
        public string? Content { get; set; }

        public string? Type { get; set; }

        public Quote ToQuote()
        {
            return new Quote()
            {
                QuoteId = Guid.NewGuid(),
                QuoteContent = Content,
                QuoteType = (QuoteType)Enum.Parse(typeof(QuoteType), Type)
            };
        }
    }
}
