using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _db;

        public QuoteRepository(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<List<Quote>> GetAllQuotesAsync()
        {
            return await _db.Quotes.ToListAsync();
        }

        public async Task<Quote?> GetQuoteByQuoteIdAsync(Guid quoteId)
        {
            return await _db.Quotes.FirstOrDefaultAsync(quote => quote.QuoteId == quoteId);
        }

        public async Task AddQuoteAsync(Quote quote)
        {
            _db.Quotes.Add(quote);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteQuoteAsync(Quote quote)
        {
            _db.Quotes.Remove(quote);
            await _db.SaveChangesAsync();
        }
    }
}
