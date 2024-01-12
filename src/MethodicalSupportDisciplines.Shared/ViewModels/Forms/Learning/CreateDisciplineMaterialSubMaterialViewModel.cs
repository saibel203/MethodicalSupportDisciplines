using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

public class CreateDisciplineMaterialSubMaterialViewModel
{
    [Required(ErrorMessage = "RequiredMaterialTypeId")]
    [Display(Name = "MaterialTypeIdFieldName")]
    public int MaterialTypeId { get; set; }

    [Required(ErrorMessage = "RequiredMaterialPath")]
    public string MaterialPath { get; set; } = string.Empty;

    public int DisciplineId { get; set; }
    public int DisciplineMaterialId { get; set; }
    
    public IFormFile? FormFile { get; set; }
}