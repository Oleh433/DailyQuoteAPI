using DailyQuote.Application.ServiceContracts;
using DailyQuote.Application.Services;
using DailyQuote.Domain.RepositoryContracts;
using DailyQuote.Infrastructure;
using DailyQuote.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DailyQuote.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

            builder.Services.AddScoped<IQuoteService, QuoteService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
            });

            var app = builder.Build();

            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
