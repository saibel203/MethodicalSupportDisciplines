namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class NewDisciplineMaterialDto
{
    public int DisciplineMaterialId { get; set; }
    public string DisciplineMaterialName { get; set; } = string.Empty;
    public string DisciplineMaterialDescription { get; set; } = string.Empty;
    public int DisciplineMaterialTypeId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int DisciplineId { get; set; }
}