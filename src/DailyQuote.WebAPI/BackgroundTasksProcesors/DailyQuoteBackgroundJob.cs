using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using Quartz;

namespace DailyQuote.WebAPI.BackgroundTasksProcesors
{
    public class DailyQuoteBackgroundJob : IJob
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IQuoteService _quoteService;
        private readonly ISubscribedUserService _subscribedUserService;

        public DailyQuoteBackgroundJob(IEmailSendingService emailSendingService, IQuoteService quoteService, ISubscribedUserService subscribedUserService)
        {
            _emailSendingService = emailSendingService;
            _quoteService = quoteService;
            _subscribedUserService = subscribedUserService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            IEnumerable<string> emails = await _subscribedUserService.GetAllUsersEmails();

            foreach (string email in emails)
            {
                QuoteResponse quote = await _quoteService.GetRandomQuoteAsync();

                await _emailSendingService.SendQuoteAsync(quote, email);
            }
        }
    }
}
