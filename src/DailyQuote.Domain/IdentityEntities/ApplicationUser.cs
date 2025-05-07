using DailyQuote.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DailyQuote.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Quote> FavouriteQuotes { get; set; } = new List<Quote>();
    }
}
