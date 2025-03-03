using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IQuoteService
    {
        Task<List<QuoteResponse>> GetAllQuotesAsync();
        Task<QuoteResponse?> GetQuoteByIdAsync(Guid quoteId);
        Task AddQuoteAsync(QuoteAddRequest quote);
        Task DeleteQuoteAsync(QuoteDeleteRequest quote);
        Task<QuoteResponse> GetRandomQuoteAsync();
    }
}
