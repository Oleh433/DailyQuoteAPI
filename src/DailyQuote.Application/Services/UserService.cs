using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace DailyQuote.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(UserRegisterRequest userRegisterRequest)
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
    }
}
