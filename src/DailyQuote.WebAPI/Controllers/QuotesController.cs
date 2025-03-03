using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("[controller]")]
    public class QuotesController : Controller
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Route("[action]")]
        public async Task<IActionResult> Random()
        {
            QuoteResponse quote = await _quoteService.GetRandomQuoteAsync();

            return Json(quote);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task Quotes(QuoteAddRequest quoteAddRequest)
        {
            await _quoteService.AddQuoteAsync(quoteAddRequest);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task DeleteQuote(QuoteDeleteRequest quoteDeleteRequest)
        {
            await _quoteService.DeleteQuoteAsync(quoteDeleteRequest);
        }
    }
}
