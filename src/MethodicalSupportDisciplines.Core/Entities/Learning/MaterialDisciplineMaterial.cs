namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class MaterialDisciplineMaterial
{
    public int MaterialId { get; set; }
    public int DisciplineMaterialId { get; set; }

    public Material Material { get; set; } = null!;
    public DisciplineMaterial DisciplineMaterial { get; set; } = null!;
}