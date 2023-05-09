using System.ComponentModel.DataAnnotations;

namespace USER_API.Resources;

public class RegisterUserResource
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
}