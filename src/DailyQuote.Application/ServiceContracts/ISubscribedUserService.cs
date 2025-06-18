using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Application.ServiceContracts
{
    public interface ISubscribedUserService
    {
        Task SubscribeAsync(string email);

        Task UnsubscribeAsync(string email);

        Task<IEnumerable<string>> GetAllUsersEmails();
    }
}
