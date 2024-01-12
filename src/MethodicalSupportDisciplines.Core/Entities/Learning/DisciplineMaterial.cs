using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class DisciplineMaterial
{
    public int DisciplineMaterialId { get; set; }
    public string DisciplineMaterialName { get; set; } = string.Empty;
    public string DisciplineMaterialDescription { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; } = null!;

    public int DisciplineMaterialTypeId { get; set; }
    public DisciplineMaterialType DisciplineMaterialType { get; set; } = null!;
    
    public ICollection<MaterialDisciplineMaterial> Materials { get; set; } = new List<MaterialDisciplineMaterial>();
}