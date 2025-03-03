using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetAllQuotesAsync();
        Task<Quote?> GetQuoteByQuoteIdAsync(Guid quoteId);
        Task AddQuoteAsync(Quote quote);
        Task DeleteQuoteAsync(Quote quote);
        Task<Quote> GetRandomQuoteAsync();
    }
}
