using DailyQuote.Application.ServiceContracts;
using DailyQuote.Application.Services;
using DailyQuote.Domain.IdentityEntities;
using DailyQuote.Domain.RepositoryContracts;
using DailyQuote.Infrastructure;
using DailyQuote.Infrastructure.Identity;
using DailyQuote.Infrastructure.Repositories;
using DailyQuote.WebAPI.BackgroundTasksProcesors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quartz;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace DailyQuote.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();

            builder.Services.AddScoped<IUserQuoteRepository, UserQuoteRepository>();

            builder.Services.AddScoped<ISubscribedUserRepository, SubscribedUserRepository>();

            builder.Services.AddScoped<IQuoteService, QuoteService>();

            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IUserQuoteService, UserQuoteService>();

            builder.Services.AddScoped<ISubscribedUserService, SubscribedUserService>();

            builder.Services.AddScoped<IEmailSendingService, EmailSendingService>();

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
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                        .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            //Quartz
            builder.Services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(DailyQuoteBackgroundJob));

                options
                    .AddJob<DailyQuoteBackgroundJob>(jobKey)
                    .AddTrigger(trigger =>
                        trigger
                            .ForJob(jobKey)
                            .WithIdentity("DailyQuoteTrigger")
                            .WithCronSchedule("0 0 12 * * ?"));
            });

            builder.Services.AddQuartzHostedService();

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
