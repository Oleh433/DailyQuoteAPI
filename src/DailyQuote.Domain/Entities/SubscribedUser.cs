using System.ComponentModel.DataAnnotations;

namespace DailyQuote.Domain.Entities
{
    public class SubscribedUser
    {
        [Key]
        public string Email { get; set; }
    }
}
