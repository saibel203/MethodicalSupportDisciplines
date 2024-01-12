using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class DisciplineMaterialType
{
    public int DisciplineMaterialTypeId { get; set; }
    public string DisciplineMaterialTypeName { get; set; } = string.Empty;

    public ICollection<DisciplineMaterial> DisciplineMaterials { get; set; } = new List<DisciplineMaterial>();
}