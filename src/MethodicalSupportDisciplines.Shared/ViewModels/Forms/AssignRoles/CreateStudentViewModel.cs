using System.ComponentModel.DataAnnotations;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.AssignRoles;

public class CreateStudentViewModel
{
    [Required(ErrorMessage = "FormatLearningRequired")]
    [Display(Name = "FormatLearningFieldName")]
    public int FormatLearningId { get; set; }
    
    [Required(ErrorMessage = "LearningStatusRequired")]
    [Display(Name = "LearningStatusFieldName")]
    public int LearningStatusId { get; set; }
    
    [Required(ErrorMessage = "FacultyRequired")]
    [Display(Name = "FacultyFieldName")]
    public int FacultyId { get; set; }
    
    [Required(ErrorMessage = "SpecialtyRequired")]
    [Display(Name = "SpecialityFieldName")]
    public int SpecialtyId { get; set; }
    
    [Required(ErrorMessage = "GroupRequired")]
    [Display(Name = "GroupFieldName")]
    public int GroupId { get; set; }

    public IReadOnlyList<FormatLearningDto> FormatLearnings { get; set; } = new List<FormatLearningDto>();
    public IReadOnlyList<LearningStatusDto> LearningStatuses { get; set; } = new List<LearningStatusDto>();
    public IReadOnlyList<FacultyDto> Faculties { get; set; } = new List<FacultyDto>();
    public IReadOnlyList<SpecialityDto> Specialities { get; set; } = new List<SpecialityDto>();
    public IReadOnlyList<GroupDto> Groups { get; set; } = new List<GroupDto>();
    
    public int GuestUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
}