using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("api/quotes")]
    public class QuotesController : Controller
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet("random")]
        public async Task<IActionResult> Random()
        {
            QuoteResponse quote = await _quoteService.GetRandomQuoteAsync();

            return Json(quote);
        }

        [HttpPost]
        public async Task<IActionResult> Quotes([FromBody] QuoteAddRequest quoteAddRequest)
        {
            await _quoteService.AddQuoteAsync(quoteAddRequest);

            return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQuote(Guid quoteId)
        {
            await _quoteService.DeleteQuoteAsync(quoteId);

            return NoContent();
        }
    }
}
