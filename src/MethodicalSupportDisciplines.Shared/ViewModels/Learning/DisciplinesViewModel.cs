using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Learning;

public class DisciplinesViewModel : SettingsViewModel
{
    public IReadOnlyList<DisciplineActionDto> Disciplines { get; set; } = new List<DisciplineActionDto>();
    public DisciplineActionDto Discipline { get; set; } = null!;
    public int DisciplineMaterialsCount { get; set; }
    public CreateDisciplineViewModel CreateDisciplineViewModel { get; set; } = null!;
}