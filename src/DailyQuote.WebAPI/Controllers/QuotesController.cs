using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("api/quotes")]
    [Authorize]
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
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> AddQuote(
            [FromBody] QuoteAddRequest quoteAddRequest)
        {
            await _quoteService.AddQuoteAsync(quoteAddRequest);

            return Created();
        }

        [HttpDelete("{quoteId:guid}")]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> DeleteQuote([FromRoute] Guid quoteId)
        {
            await _quoteService.DeleteQuoteAsync(quoteId);

            return NoContent();
        }

        [HttpPost("mark-as-favourite/{quoteId:guid}")]
        public async Task<IActionResult> MarkAsFavourite(Guid quoteId)
        {
            await _quoteService.AddToFavouriteAsync(quoteId);

            return Ok();
        }

        [HttpPost("unmark-from-favourite/{quoteId:guid}")]
        public async Task<IActionResult> UnmarkFromFavourite(Guid quoteId)
        {
            await _quoteService.RemoveFromFavouriteAsync(quoteId);

            return Ok();
        }

        [HttpGet("get-favourite")]
        public async Task<IActionResult> GetFavourite()
        {
            List<QuoteResponse> favouriteQuotes = await _quoteService.GetFavouriteAsync();

            return Json(favouriteQuotes);
        }
    }
}
