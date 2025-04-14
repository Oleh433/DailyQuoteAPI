using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IUserService
    {
        Task UserRegisterAsync(UserRegisterRequest userRegisterRequest);

        Task AdminRegisterAsync(UserRegisterRequest userRegisterRequest);

        Task SignInAsync(UserSignInRequest userSignInRequest);

        Task SignOutAsync();
    }
}
