using Eventure.Models.Entities;
using Eventure.Models.RequestDto;
using Eventure.Models.Results;

namespace Eventure.Services;

public interface IAuthService
{
    Task<RegisterResult> RegisterUser(RegisterUserDto registerUserDto);
    Task<LoginResult> LoginAsync(LoginUserDto loginUserDto);
    Task<IList<string>> GetRolesAsync(string userName);

}