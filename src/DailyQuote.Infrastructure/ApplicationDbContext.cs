using DailyQuote.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DailyQuote.Domain.IdentityEntities;

namespace DailyQuote.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<UserQuote> UserQuotes { get; set; }
 
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>().ToTable("Quotes");

            modelBuilder.Entity<UserQuote>().ToTable("PendingQuotes");
        }
    }
}
