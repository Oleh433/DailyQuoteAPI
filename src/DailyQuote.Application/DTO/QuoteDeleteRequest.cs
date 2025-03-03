using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.DTO
{
    public class QuoteDeleteRequest
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
