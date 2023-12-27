using System.ComponentModel.DataAnnotations;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.AssignRoles;

public class CreateTeacherViewModel
{
    [Required(ErrorMessage = "QualificationRequired")]
    [Display(Name = "QualificationFieldName")]
    public int QualificationId { get; set; }
    public int GuestUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
    
    public IReadOnlyList<QualificationDto> Qualifications { get; set; } = new List<QualificationDto>();
}