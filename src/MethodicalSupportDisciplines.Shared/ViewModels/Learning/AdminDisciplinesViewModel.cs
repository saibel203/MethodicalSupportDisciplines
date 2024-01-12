using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Additional;

namespace MethodicalSupportDisciplines.Shared.ViewModels.Learning;

public class AdminDisciplinesViewModel : SettingsViewModel
{
    public IEnumerable<DisciplineActionDto> Disciplines { get; set; } = new List<DisciplineActionDto>();
}