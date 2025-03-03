using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyQuote.Domain;
using DailyQuote.Domain.RepositoryContracts;

namespace DailyQuote.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task AddQuoteAsync(Quote quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            await _quoteRepository.AddQuoteAsync(quote);
        }

        public async Task DeleteQuoteAsync(Quote quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            await _quoteRepository.DeleteQuoteAsync(quote);
        }

        public async Task<List<Quote>> GetAllQuotesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Quote?> GetQuoteByQuoteIdAsync(Guid quoteId)
        {
            if (quoteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quoteId));
            }

            return await _quoteRepository.GetQuoteByQuoteIdAsync(quoteId);
        }

        public async Task<Quote> GetRandomQuoteAsync()
        {
            Random random = new Random();
            int recordsCount = await _quoteRepository.GetRecordsCountAsync();

            return await _quoteRepository.GetRandomQuoteAsync(random.Next(0, recordsCount));
        }
    }
}
