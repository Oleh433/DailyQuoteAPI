using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DailyQuote.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task RegisterAsync(UserRegisterRequest userRegisterRequest)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRegisterRequest.Email,
                Email = userRegisterRequest.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, userRegisterRequest.Password!);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(error => error.Description)));
            }
        }

        public async Task SignInAsync(UserSignInRequest userSignInRequest)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(userSignInRequest.Email, userSignInRequest.Password, false, false);

            if (!signInResult.Succeeded)
            {
                throw new InvalidOperationException(signInResult.ToString());
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
