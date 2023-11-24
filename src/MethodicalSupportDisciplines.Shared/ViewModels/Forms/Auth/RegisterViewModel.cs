using System.ComponentModel.DataAnnotations;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "UsernameRequired")] 
    [StringLength(20, MinimumLength = 6, ErrorMessage = "UsernameLength")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "PasswordRequired")]
    [MinLength(6, ErrorMessage = "PasswordMinLength")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", 
        ErrorMessage = "PasswordRegularExpression")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "ConfirmPasswordRequired")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "PasswordsNotMatch")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    /*[Required (ErrorMessage = "SelectedRole поле обов'язвове")] 
    public string SelectedRole { get; set; } = string.Empty;*/

    public string ReturnUrl { get; set; } = string.Empty;
}