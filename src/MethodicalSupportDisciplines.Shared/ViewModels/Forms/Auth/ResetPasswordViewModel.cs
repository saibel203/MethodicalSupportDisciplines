using System.ComponentModel.DataAnnotations;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

public class ResetPasswordViewModel
{
    [Required]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress]
    public string Value { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "NewPasswordRequired")]
    [MinLength(6, ErrorMessage = "PasswordMinLength")]
    [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$", 
        ErrorMessage = "PasswordRegularExpression")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "ConfirmNewPasswordRequired")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "PasswordsNotMatch")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}