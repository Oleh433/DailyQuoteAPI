using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IEmailSendingService
    {
        Task SendQuoteAsync(QuoteResponse quote, string email);
    }
}
