using System.ComponentModel.DataAnnotations;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

public class RemindPasswordViewModel
{
    [Required(ErrorMessage = "Email поле є обов'язвовим")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}