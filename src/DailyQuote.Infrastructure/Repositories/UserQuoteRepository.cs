using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DailyQuote.Infrastructure.Repositories
{
    public class UserQuoteRepository : IUserQuoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserQuoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserQuote userQuote)
        {
            await _dbContext.UserQuotes.AddAsync(userQuote);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserQuote userQuote)
        {
            _dbContext.UserQuotes.Remove(userQuote);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserQuote>> GetAllAsync()
        {
            return await _dbContext.UserQuotes.ToListAsync();
        }

        public async Task<UserQuote?> GetByIdAsync(Guid userQuoteId)
        {
            return await _dbContext.UserQuotes
                .FirstOrDefaultAsync(quote => quote.Id == userQuoteId);
        }
    }
}
