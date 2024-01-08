using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class MaterialType
{
    public int MaterialTypeId { get; set; }
    public string MaterialTypeName { get; set; } = string.Empty;

    public ICollection<Material> Materials { get; set; } = new List<Material>();
}