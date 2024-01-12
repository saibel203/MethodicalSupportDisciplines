namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class NewMaterialDto
{
    public int MaterialId { get; set; }
    public int MaterialTypeId { get; set; }
    public string MaterialPath { get; set; } = string.Empty;
}