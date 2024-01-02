using MethodicalSupportDisciplines.Shared.AdditionalModels;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

public class CreateDisciplineViewModel : QueryParameters
{
    public string DisciplineName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string DisciplineDescription { get; set; } = string.Empty;
}