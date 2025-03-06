using DailyQuote.Application.DTO;

namespace DailyQuote.Application.ServiceContracts
{
    public interface IUserService
    {
        Task Register(UserRegisterRequest userRegisterRequest);
    }
}
