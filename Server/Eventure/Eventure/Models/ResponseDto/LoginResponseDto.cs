namespace Eventure.Models.ResponseDto;

public class LoginResponseDto
{
    public IList<string> Roles { get; set; }
    public string UserName { get; set; }
}