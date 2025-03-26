using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterRequest userRegisterRequest);

        Task SignInAsync(UserSignInRequest userSignInRequest);

        Task SignOutAsync();
    }
}
