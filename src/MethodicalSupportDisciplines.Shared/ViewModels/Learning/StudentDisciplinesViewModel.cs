using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Learning;

public class StudentDisciplinesViewModel : SettingsViewModel
{
    public IReadOnlyList<DisciplineGroupActionDto> DisciplineGroups { get; set; } = 
        new List<DisciplineGroupActionDto>();

    public DisciplineActionDto Discipline { get; set; } = null!;
}