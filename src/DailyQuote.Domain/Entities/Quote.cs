using DailyQuote.Domain.Enums;

namespace DailyQuote.Domain.Entities
{
    public class Quote
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public QuoteType Type { get; set; }

        public DateTime LastShownTime { get; set; }
    }
}
