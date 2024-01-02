namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class NewDisciplineDto
{
    public string DisciplineName { get; set; } = string.Empty;
    public string DisciplineDescription { get; set; } = string.Empty;
    public int TeacherId { get; set; }
}