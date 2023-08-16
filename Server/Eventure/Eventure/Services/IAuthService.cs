using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterUserDto registerUserDto);
    Task<LoginResult> LoginAsync(LoginUserDto loginUserDto);
    Task<IList<string>> GetRolesAsync(string userName);
}