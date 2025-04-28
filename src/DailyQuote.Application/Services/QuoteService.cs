using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
using DailyQuote.Domain.IdentityEntities;
using DailyQuote.Domain.RepositoryContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DailyQuote.Application.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuoteService(IQuoteRepository quoteRepository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _quoteRepository = quoteRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddQuoteAsync(QuoteAddRequest quoteAddRequest)
        {
            if (quoteAddRequest is null)
            {
                throw new ArgumentNullException(nameof(quoteAddRequest));
            }

            Quote quote = quoteAddRequest.ToQuote();
            await _quoteRepository.AddAsync(quote);
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

        public async Task AddToFavouriteAsync(Guid quoteId)
        {
            Quote? quote = await _quoteRepository.GetByIdAsync(quoteId);

            if (quote is null)
            {
                throw new KeyNotFoundException("Quote does not exist");
            }

            Guid currentUserId = Guid.Parse(
                _userManager.GetUserId(_httpContextAccessor.HttpContext.User));

            ApplicationUser user = await _userManager.Users
                .FirstAsync(user => user.Id == currentUserId);

            user.FavouriteQuotes.Add(quote);
            await _userManager.UpdateAsync(user);
        }

        public async Task RemoveFromFavouriteAsync(Guid quoteId)
        {
            Quote? quote = await _quoteRepository.GetByIdAsync(quoteId);

            if (quote is null)
            {
                throw new KeyNotFoundException("Quote does not exist");
            }

            Guid currentUserId = Guid.Parse(
                _userManager.GetUserId(_httpContextAccessor.HttpContext.User));

            ApplicationUser user = await _userManager.Users
                .FirstAsync(user => user.Id == currentUserId);

            user.FavouriteQuotes.Remove(quote);
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<QuoteResponse>> GetFavouriteAsync()
        {
            Guid currentUserId = Guid.Parse(
                _userManager.GetUserId(_httpContextAccessor.HttpContext.User));

            ApplicationUser user = await _userManager.Users
                .Include(user => user.FavouriteQuotes)
                .FirstAsync(user => user.Id == currentUserId);

            List<QuoteResponse> favouriteQuotes = user.FavouriteQuotes
                .Select(quote => quote.ToQuoteResponse()).ToList();

            return favouriteQuotes;
        }
    }
}
