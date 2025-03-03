using DailyQuote.Domain.Entities;

namespace DailyQuote.Application.DTO
{
    public class QuoteUpdateRequest
    {
        public Guid QuoteId { get; set; }

        public string? QuoteContent { get; set; }

        public QuoteType? QuoteType { get; set; }

        public Quote ToQuote()
        {
            return new Quote()
            {
                QuoteId = QuoteId,
                QuoteContent = QuoteContent,
                QuoteType = QuoteType
            };
        }
    }
}
