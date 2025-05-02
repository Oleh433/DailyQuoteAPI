using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IUserQuoteService
    {
        Task AddQuoteAsync(QuoteAddRequest quoteAddRequest);

        Task ApproveQuoteAsync(Guid quoteId);

        Task RejectQuoteAsync(Guid quoteId);

        Task<IEnumerable<QuoteResponse>> GetAllAsync();
    }
}
