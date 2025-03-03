using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyQuote.Domain;
using DailyQuote.Domain.RepositoryContracts;
using DailyQuote.Application.DTO;

namespace DailyQuote.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task AddQuoteAsync(QuoteAddRequest quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            await _quoteRepository.AddQuoteAsync(quote.ToQuote());
        }

        public async Task DeleteQuoteAsync(QuoteDeleteRequest quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            await _quoteRepository.DeleteQuoteAsync(quote.ToQuote());
        }

        public async Task<List<QuoteResponse>> GetAllQuotesAsync()
        {
            List<Quote> quotes = await _quoteRepository.GetAllQuotesAsync();

            return quotes.Select(quote => quote.ToQuoteResponse()).ToList();
        }

        public async Task<QuoteResponse?> GetQuoteByIdAsync(Guid quoteId)
        {
            if (quoteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(quoteId));
            }

            return (await _quoteRepository.GetQuoteByQuoteIdAsync(quoteId)).ToQuoteResponse();
        }

        public async Task<QuoteResponse> GetRandomQuoteAsync()
        {
            Random random = new Random();
            int recordsCount = await _quoteRepository.GetRecordsCountAsync();

            return (await _quoteRepository.GetRandomQuoteAsync(random.Next(0, recordsCount))).ToQuoteResponse();
        }
    }
}
