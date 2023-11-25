namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class DisciplineGroup
{
    public int DisciplineId { get; set; }
    public int GroupId { get; set; }
    public Discipline Discipline { get; set; } = null!;
    public Group Group { get; set; } = null!;
}