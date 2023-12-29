namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class Material
{
    public int MaterialId { get; set; }
    public string? MaterialPath { get; set; }
    public string? MaterialUrl { get; set; }
    public string? MaterialBook { get; set; }

    public ICollection<MaterialDisciplineMaterial> Materials { get; set; } = new List<MaterialDisciplineMaterial>();
}