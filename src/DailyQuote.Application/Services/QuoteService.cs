using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
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

        public async Task AddQuoteAsync(QuoteAddRequest quote)
        {
            if (quote is null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            await _quoteRepository.AddAsync(quote.ToQuote());
        }

        public async Task DeleteQuoteAsync(Guid quoteId)
        {
            Quote? quote = await _quoteRepository.GetByIdAsync(quoteId);

            if (quote is null)
            {
                throw new KeyNotFoundException("Unable to delete because quote does not exist");
            }

            await _quoteRepository.DeleteAsync(quote);
        }

        public async Task<List<QuoteResponse>> GetAllQuotesAsync()
        {
            IEnumerable<Quote> quotes = await _quoteRepository.GetAllAsync();

            return quotes.Select(quote => quote.ToQuoteResponse()).ToList();
        }

        public async Task<QuoteResponse?> GetQuoteByIdAsync(Guid quoteId)
        {
            Quote? quote = await _quoteRepository.GetByIdAsync(quoteId);

            if (quote is null)
            {
                throw new KeyNotFoundException("Quote does not exist");
            }

            return quote.ToQuoteResponse();
        }

        public async Task<QuoteResponse> GetRandomQuoteAsync()
        {
            IEnumerable<Quote> quotes = await _quoteRepository.GetRandomAsync();

            if (quotes.Count() == 0)
            {
                throw new InvalidOperationException("Quotes collection does not contain elements");
            }

            return quotes.First().ToQuoteResponse();
        }
    }
}
