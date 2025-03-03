using DailyQuote.Domain.Entities;

namespace DailyQuote.Application.DTO
{
    public class QuoteAddRequest
    {
        public string? Content { get; set; }

        public string? Type { get; set; }

        public Quote ToQuote()
        {
            if (!Enum.IsDefined(typeof(QuoteType), Type))
            {
                throw new ArgumentException("Provided type does not exist");
            }

            return new Quote()
            {
                QuoteId = Guid.NewGuid(),
                QuoteContent = Content,
                QuoteType = (QuoteType)Enum.Parse(typeof(QuoteType), Type)
            };
        }
    }
}
