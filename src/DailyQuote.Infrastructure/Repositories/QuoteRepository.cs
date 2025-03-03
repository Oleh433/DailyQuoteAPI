using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace DailyQuote.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuoteRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Quote>> GetAllAsync()
        {
            return await _dbContext.Quotes.ToListAsync();
        }

        public async Task<Quote?> GetByIdAsync(Guid quoteId)
        {
            return await _dbContext.Quotes
                .FindAsync(keyValues: [quoteId]);
        }

        public async Task AddAsync(Quote quote)
        {
            await _dbContext.Quotes.AddAsync(quote);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Quote quote)
        {
            _dbContext.Quotes.Remove(quote);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Quote>> GetRandomAsync(int amount = 1)
        {
            var sql = """
                SELECT TOP ({0}) *
                FROM {1}
                ORDER BY NEWID();
            """;

            var formattedSql = string.Format(
                sql,
                amount,
                nameof(ApplicationDbContext.Quotes));

            return await _dbContext.Quotes
                .FromSqlRaw(formattedSql)
                .ToListAsync();
        }

        public async Task<int> GetRecordsCountAsync()
        {
            return await _dbContext.Quotes
                .CountAsync();
        }
    }
}
