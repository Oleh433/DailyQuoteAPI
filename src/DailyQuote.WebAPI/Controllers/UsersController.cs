using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyQuote.WebAPI.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISubscribedUserService _subscribedUserService;

        public UsersController(IUserService userService, ISubscribedUserService subscribedUserService)
        {
            _userService = userService;
            _subscribedUserService = subscribedUserService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegisterRequest userRegisterRequest)
        {
            await _userService.UserRegisterAsync(userRegisterRequest);

            return Created();
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> RegisterAdmin(
            [FromBody] UserRegisterRequest userRegisterRequest)
        {
            await _userService.AdminRegisterAsync(userRegisterRequest);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] UserSignInRequest userSignInRequest)
        {
            await _userService.SignInAsync(userSignInRequest);

            return Ok();
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            
            return Ok();
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] string email)
        {
            await _subscribedUserService.SubscribeAsync(email);

            return Ok();
        }

        [HttpPut("unsubscribe")]
        public async Task<IActionResult> Unubscribe([FromBody] string email)
        {
            await _subscribedUserService.UnsubscribeAsync(email);

            return Ok();
        }
    }
}
