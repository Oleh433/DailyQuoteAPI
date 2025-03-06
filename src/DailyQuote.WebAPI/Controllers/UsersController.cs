using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            await _userService.Register(userRegisterRequest);

            return Created();
        }
    }
}
