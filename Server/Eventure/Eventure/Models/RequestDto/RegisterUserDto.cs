using System.ComponentModel.DataAnnotations;

namespace Eventure.Models.RequestDto;

public class RegisterUserDto
{
   [Required]
   [MinLength(4, ErrorMessage = "Username must be at least 4 characters long.")]
   [RegularExpression(@"^[a-zA-Z0-9_]*$", 
       ErrorMessage = "Username can only contain alphanumeric characters and underscores.")]
    public string Username { get; set; }
    
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$", 
        ErrorMessage = "Password must be at least 8 characters long and contain at least 1 special character, 1 capital letter, and 1 number.")]
    public string Password { get; set; }
}