using System.ComponentModel.DataAnnotations;
using DailyQuote.Domain.Enums;

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
}
