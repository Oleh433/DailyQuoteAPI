using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using DailyQuote.Domain.Enums;
using DailyQuote.Domain.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DailyQuote.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UserRegisterAsync(UserRegisterRequest userRegisterRequest)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRegisterRequest.Email,
                Email = userRegisterRequest.Email
            };

            IdentityResult userRegistrationResult = await _userManager.CreateAsync(user, userRegisterRequest.Password);
            
            if (!userRegistrationResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", userRegistrationResult.Errors.Select(error => error.Description)));
            }

            IdentityResult roleApplyingResult = await _userManager.AddToRoleAsync(user, RoleOptions.User.ToString());

            if (!roleApplyingResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", roleApplyingResult.Errors.Select(error => error.Description)));
            }
        }

        public async Task AdminRegisterAsync(UserRegisterRequest userRegisterRequest)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = userRegisterRequest.Email,
                Email = userRegisterRequest.Email
            };

            IdentityResult userRegistrationResult = await _userManager.CreateAsync(user, userRegisterRequest.Password);

            if (!userRegistrationResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", userRegistrationResult.Errors.Select(error => error.Description)));
            }

            IdentityResult roleApplyingResult = await _userManager.AddToRoleAsync(user, RoleOptions.Admin.ToString());

            if (!roleApplyingResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", roleApplyingResult.Errors.Select(error => error.Description)));
            }
        }

        public async Task SignInAsync(UserSignInRequest userSignInRequest)
        {
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(
                userSignInRequest.Email, 
                userSignInRequest.Password, 
                false, 
                false);

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
