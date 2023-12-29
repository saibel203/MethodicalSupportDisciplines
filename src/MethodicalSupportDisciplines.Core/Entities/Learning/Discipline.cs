using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class Discipline
{
    public int DisciplineId { get; set; }
    public string DisciplineName { get; set; } = string.Empty;

    public int TeacherId { get; set; }
    public TeacherUser Teacher { get; set; } = null!;
    
    public ICollection<DisciplineGroup> DisciplineGroups { get; set; } = new List<DisciplineGroup>();
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
    public ICollection<DisciplineMaterial> DisciplineMaterials { get; set; } = new List<DisciplineMaterial>();
}