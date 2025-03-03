using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.DTO
{
    public class QuoteResponse
    {
        public Guid QuoteId { get; set; }
        public string? QuoteContent { get; set; }
        public QuoteType? QuoteType { get; set; }
    }

    public static class QuoteExtensions
    {
        public static QuoteResponse ToQuoteResponse(this Quote quote)
        {
            return new QuoteResponse
            { 
                QuoteId = quote.QuoteId, 
                QuoteContent = quote.QuoteContent, 
                QuoteType = quote.QuoteType 
            };
        }
    }
}
