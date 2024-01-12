using MethodicalSupportDisciplines.Shared.Dto.Additional;
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

    public CreateDisciplineMaterialSubMaterialViewModel CreateDisciplineMaterialSubMaterialViewModel { get; set; } =
        null!;

    public CreateDisciplineMaterialViewModel CreateDisciplineMaterialViewModel { get; set; } = null!;

    public CreateDisciplineGroupViewModel CreateDisciplineGroupViewModel { get; set; } = null!;

    public IReadOnlyList<MaterialTypeDto> MaterialTypes { get; set; } = new List<MaterialTypeDto>();

    public IReadOnlyList<GroupDto> Groups { get; set; } =
        new List<GroupDto>();

    public IReadOnlyList<DisciplineMaterialTypeDto> DisciplineMaterialTypes { get; set; } =
        new List<DisciplineMaterialTypeDto>();
}