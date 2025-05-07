using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;
using System.Linq;

namespace DailyQuote.Infrastructure.Repositories
{
    public class SubscribedUserRepository : ISubscribedUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscribedUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SubscribeAsync(SubscribedUser subscribedUser)
        {
            await _dbContext.SubscribedUsers.AddAsync(subscribedUser);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(SubscribedUser subscribedUser)
        {
            _dbContext.SubscribedUsers.Remove(subscribedUser);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(string Email)
        {
            return _dbContext.SubscribedUsers
                .Any(user => user.Email == Email);
        }
    }
}
