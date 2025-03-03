using System.ComponentModel.DataAnnotations;

namespace DailyQuote.Domain.Entities
{
    public class Quote
    {
        [Key]
        public Guid QuoteId { get; set; }

        public string? QuoteContent { get; set; }

        public QuoteType? QuoteType { get; set; }

        public DateTime? LastShownTime { get; set; }
    }

    public enum QuoteType
    {
        Motivational,
        Humor,
        Philosophical,
        Love,
        Life
    }
}
