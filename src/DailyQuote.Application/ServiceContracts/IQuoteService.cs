using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IQuoteService
    {
        Task<List<QuoteResponse>> GetAllQuotesAsync();

        Task<QuoteResponse?> GetQuoteByIdAsync(Guid quoteId);

        Task AddQuoteAsync(QuoteAddRequest quote);

        Task DeleteQuoteAsync(Guid quoteId);

        Task<QuoteResponse> GetRandomQuoteAsync();
    }
}
