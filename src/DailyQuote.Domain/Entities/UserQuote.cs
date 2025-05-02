using DailyQuote.Domain.Enums;

namespace DailyQuote.Domain.Entities
{
    public class UserQuote
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public QuoteType Type { get; set; }
    }
}
