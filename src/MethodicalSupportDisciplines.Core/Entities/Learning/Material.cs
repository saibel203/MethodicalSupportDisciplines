using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class Material
{
    public int MaterialId { get; set; }
    
    public int MaterialTypeId { get; set; }
    public MaterialType MaterialType { get; set; } = null!;

    public string MaterialPath { get; set; } = string.Empty;

    public ICollection<MaterialDisciplineMaterial> Materials { get; set; } = new List<MaterialDisciplineMaterial>();
}