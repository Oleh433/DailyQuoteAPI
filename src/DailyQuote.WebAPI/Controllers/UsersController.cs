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
            await _userService.RegisterAsync(userRegisterRequest);

            UserSignInRequest userSignInRequest = new UserSignInRequest()
            {
                Email = userRegisterRequest.Email,
                Password = userRegisterRequest.Password
            };

            await _userService.SignInAsync(userSignInRequest);

            return Created();
        }

        //Method?
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserSignInRequest userSignInRequest)
        {
            await _userService.SignInAsync(userSignInRequest);

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return Ok();
        }
    }
}
