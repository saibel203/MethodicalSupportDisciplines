using System.ComponentModel.DataAnnotations;
using MethodicalSupportDisciplines.Shared.AdditionalModels;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

public class CreateDisciplineViewModel : QueryParameters
{
    [Required(ErrorMessage = "NameRequired")]
    [MaxLength(100, ErrorMessage = "NameMaxLength")]
    public string DisciplineName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "DescriptionRequired")]
    [MaxLength(5000, ErrorMessage = "DescriptionMaxLength")]
    public string DisciplineDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "IdRequired")]
    public int TeacherId { get; set; }
}