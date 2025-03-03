using DailyQuote.Domain.Entities;

namespace DailyQuote.Domain.RepositoryContracts
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> GetAllAsync();

        Task<Quote?> GetByIdAsync(Guid quoteId);

        Task AddAsync(Quote quote);

        Task DeleteAsync(Quote quote);

        Task<IEnumerable<Quote>> GetRandomAsync(int amount = 1);

        Task<int> GetRecordsCountAsync();
    }
}
