using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;

namespace DailyQuote.Application.Services
{
    public class UserQuoteService : IUserQuoteService
    {
        private readonly IUserQuoteRepository _userQuoteRepository;
        private readonly IQuoteService _quoteService;

        public UserQuoteService(IUserQuoteRepository userQuoteRepository, IQuoteService quoteService)
        {
            _userQuoteRepository = userQuoteRepository;
            _quoteService = quoteService;
        }

        public async Task AddQuoteAsync(QuoteAddRequest quoteAddRequest)
        {
            if (quoteAddRequest is null)
            {
                throw new ArgumentNullException(nameof(quoteAddRequest));
            }

            UserQuote userQuote = quoteAddRequest.ToUserQuote();
            await _userQuoteRepository.AddAsync(userQuote);
        }

        public async Task ApproveQuoteAsync(Guid quoteId)
        {
            UserQuote? userQuote = await _userQuoteRepository.GetByIdAsync(quoteId);

            if (userQuote is null)
            {
                throw new KeyNotFoundException(nameof(quoteId));
            }

            QuoteAddRequest quoteAddRequest = new QuoteAddRequest()
            {
                Content = userQuote.Content,
                Type = userQuote.Type.ToString()
            };

            await _quoteService.AddQuoteAsync(quoteAddRequest);

            await _userQuoteRepository.DeleteAsync(userQuote);
        }

        public async Task RejectQuoteAsync(Guid quoteId)
        {
            UserQuote? userQuote = await _userQuoteRepository.GetByIdAsync(quoteId);

            if (userQuote is null)
            {
                throw new KeyNotFoundException(nameof(quoteId));
            }

            await _userQuoteRepository.DeleteAsync(userQuote);
        }

        public async Task<IEnumerable<QuoteResponse>> GetAllAsync()
        {
            IEnumerable<UserQuote> userQuotes = await _userQuoteRepository.GetAllAsync();

            return userQuotes.Select(quote => quote.ToQuoteResponse());
        }
    }
}
