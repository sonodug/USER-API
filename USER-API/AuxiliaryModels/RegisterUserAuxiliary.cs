using System.ComponentModel.DataAnnotations;

namespace USER_API.AuxiliaryModels;

public class RegisterUserAuxiliary
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public bool IsAdmin { get; set; }
}