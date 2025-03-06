using Microsoft.AspNetCore.Identity;

namespace DailyQuote.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool ReceiveQuotes { get; set; } = false;
    }
}
