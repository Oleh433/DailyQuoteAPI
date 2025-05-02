using DailyQuote.Domain.Entities;

namespace DailyQuote.Domain.RepositoryContracts
{
    public interface IUserQuoteRepository
    {
        Task AddAsync(UserQuote userQuote);

        Task DeleteAsync(UserQuote userQuote);

        Task<UserQuote?> GetByIdAsync(Guid id);

        Task<IEnumerable<UserQuote>> GetAllAsync();
    }
}
