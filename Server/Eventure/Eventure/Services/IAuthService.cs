using Eventure.Models.RequestDto;

namespace Eventure.Services;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterUserDto registerUserDto);
}