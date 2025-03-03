using DailyQuote.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyQuote.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>().ToTable("Quotes");
        }
    }
}
