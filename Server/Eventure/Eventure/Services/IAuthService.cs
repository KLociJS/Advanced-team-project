using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterUser(RegisterUserDto registerUserDto);
}