using System.ComponentModel.DataAnnotations;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email поле обов'язкове")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password поле обов'язкове")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    public bool RememberMe { get; set; }
}