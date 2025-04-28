using DailyQuote.Application.ServiceContracts;
using DailyQuote.Application.Services;
using DailyQuote.Domain.IdentityEntities;
using DailyQuote.Domain.RepositoryContracts;
using DailyQuote.Infrastructure;
using DailyQuote.Infrastructure.Identity;
using DailyQuote.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace DailyQuote.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

            builder.Services.AddScoped<IQuoteService, QuoteService>();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IdentityInitializer>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssemblyContaining<QuoteService>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssembly(typeof(QuoteService).Assembly);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString"));
                //options.UseSqlServer(builder.Configuration["AzureDbConnectionString"]);
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                        .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IdentityInitializer>();
                await initializer.CreateRolesAsync();
                await initializer.AddOwnerAsync();
            }

            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
