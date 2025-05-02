using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("api/quotes")]
    [Authorize]
    public class QuotesController : Controller
    {
        private readonly IQuoteService _quoteService;
        private readonly IUserQuoteService _userQuoteService;

        public QuotesController(IQuoteService quoteService, IUserQuoteService userQuoteService)
        {
            _quoteService = quoteService;
            _userQuoteService = userQuoteService;
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

        [HttpPut("{quoteId:guid}/mark-as-favourite")]
        public async Task<IActionResult> MarkAsFavourite(Guid quoteId)
        {
            await _quoteService.AddToFavouriteAsync(quoteId);

            return Ok();
        }

        [HttpPut("{quoteId:guid}/unmark-from-favourite")]
        public async Task<IActionResult> UnmarkFromFavourite(Guid quoteId)
        {
            await _quoteService.RemoveFromFavouriteAsync(quoteId);

            return Ok();
        }

        [HttpGet("favourite")]
        public async Task<IActionResult> GetFavourite()
        {
            List<QuoteResponse> favouriteQuotes = await _quoteService
                .GetFavouriteAsync();

            return Json(favouriteQuotes);
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(QuoteAddRequest quoteAddRequest)
        {
            await _userQuoteService.AddQuoteAsync(quoteAddRequest);

            return Ok();
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpPost("{quoteId}/approve")]
        public async Task<IActionResult> ApprovePendingQuote(Guid quoteId)
        {
            await _userQuoteService.ApproveQuoteAsync(quoteId);

            return Ok();
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpDelete("{quoteId}/reject")]
        public async Task<IActionResult> RejectPendingQuote(Guid quoteId)
        {
            await _userQuoteService.RejectQuoteAsync(quoteId);

            return Ok();
        }

        [Authorize(Roles = "Admin,Owner")]
        [HttpGet("pending")] 
        public async Task<IActionResult> GetAllPendingQuotes()
        {
            IEnumerable<QuoteResponse> quotes = await _userQuoteService
                .GetAllAsync();

            return Json(quotes);
        }
    }
}
