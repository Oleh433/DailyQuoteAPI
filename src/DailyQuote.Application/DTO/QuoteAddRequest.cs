using DailyQuote.Domain.Entities;
using DailyQuote.Domain.Enums;

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
                Id = Guid.NewGuid(),
                Content = Content,
                Type = (QuoteType)Enum.Parse(typeof(QuoteType), Type)
            };
        }

        public UserQuote ToUserQuote()
        {
            return new UserQuote()
            {
                Id = Guid.NewGuid(),
                Content = Content,
                Type = (QuoteType)Enum.Parse(typeof(QuoteType), Type)
            };
        }
    }
}
