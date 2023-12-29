using System.ComponentModel.DataAnnotations;
using MethodicalSupportDisciplines.Shared.ValidationAttributes;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "UsernameRequired")] 
    [StringLength(20, MinimumLength = 6, ErrorMessage = "UsernameLength")]
    public string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "EmailRequired")]
    [EmailAddress(ErrorMessage = "EmailValidationError")]
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
    
    [Required(ErrorMessage = "PhoneNumberRequired")]
    [PhoneNumberValidation(ErrorMessage = "PhoneNumber не має починатися з символу '+' або починатися з " +
                                          "послідовності '+380' | '380'")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "FirstNameRequired")]
    [DataType(DataType.Text)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "LastNameRequired")]
    [DataType(DataType.Text)]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "PatronymicRequired")]
    [DataType(DataType.Text)]
    public string Patronymic { get; set; } = string.Empty;
}