using DailyQuote.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Domain.RepositoryContracts
{
    public interface ISubscribedUserRepository
    {
        Task SubscribeAsync(SubscribedUser subscribedUser);

        Task UnsubscribeAsync(SubscribedUser subscribedUser);

        Task<bool> IsExistsAsync(string Email);
    }
}
