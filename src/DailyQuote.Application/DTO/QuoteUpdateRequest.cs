using DailyQuote.Domain.Entities;
using DailyQuote.Domain.Enums;

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
                Id = QuoteId,
                Content = QuoteContent,
                Type = QuoteType.Value
            };
        }
    }
}
