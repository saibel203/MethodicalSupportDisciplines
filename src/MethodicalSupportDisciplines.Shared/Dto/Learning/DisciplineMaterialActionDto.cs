using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class DisciplineMaterialActionDto
{
    public int DisciplineMaterialId { get; set; }
    public string DisciplineMaterialName { get; set; } = string.Empty;
    public string DisciplineMaterialDescription { get; set; } = string.Empty;
    public int DisciplineMaterialType { get; set; }

    public Discipline Discipline { get; set; } = null!;
}