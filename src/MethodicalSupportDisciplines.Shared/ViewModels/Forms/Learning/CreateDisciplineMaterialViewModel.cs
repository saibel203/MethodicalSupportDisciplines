using System.ComponentModel.DataAnnotations;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

public class CreateDisciplineMaterialViewModel
{
    [Required(ErrorMessage = "RequiredDisciplineMaterialName")]
    [MaxLength(100, ErrorMessage = "DisciplineMaterialNameMaxLength")]
    public string DisciplineMaterialName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "RequiredDisciplineMaterialDescription")]
    [MaxLength(5000, ErrorMessage = "DisciplineMaterialDescriptionMaxLength")]
    public string DisciplineMaterialDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "RequiredDisciplineMaterialType")]
    [Display(Name = "DisciplineMaterialFieldName")]
    public int DisciplineMaterialTypeId { get; set; }
    
    public int DisciplineId { get; set; }
}