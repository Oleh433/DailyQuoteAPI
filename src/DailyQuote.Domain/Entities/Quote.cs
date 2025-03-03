using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Domain.Entities
{
    public class Quote
    {
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
