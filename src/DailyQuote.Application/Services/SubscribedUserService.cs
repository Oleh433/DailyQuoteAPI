using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Entities;
using DailyQuote.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.Services
{
    public class SubscribedUserService : ISubscribedUserService
    {
        private readonly ISubscribedUserRepository _subscribedUserRepository;

        public SubscribedUserService(ISubscribedUserRepository subscribedUserRepository)
        {
            _subscribedUserRepository = subscribedUserRepository;
        }

        public async Task SubscribeAsync(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (await _subscribedUserRepository.IsExistsAsync(email))
            {
                throw new InvalidOperationException("User with this Email already exists");
            }

            SubscribedUser user = new SubscribedUser() { Email = email };

            await _subscribedUserRepository.SubscribeAsync(user);
        }

        public async Task UnsubscribeAsync(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (!await _subscribedUserRepository.IsExistsAsync(email))
            {
                throw new InvalidOperationException("No registerd Users with that Email");
            }

            SubscribedUser user = new SubscribedUser() { Email = email };

            await _subscribedUserRepository.UnsubscribeAsync(user);
        }

        public async Task<IEnumerable<string>> GetAllUsersEmails()
        {
            IEnumerable<SubscribedUser> subscribedUsers = await _subscribedUserRepository
                .GetAllAsync();

            return subscribedUsers.Select(user => user.Email);
        }
    }
}
 