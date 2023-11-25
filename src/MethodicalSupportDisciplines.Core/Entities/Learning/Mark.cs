using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class Mark
{
    public int MarkId { get; set; }
    public int MarkValue { get; set; }

    public int StudentId { get; set; }
    public StudentUser Student { get; set; } = null!;

    public int TeacherId { get; set; }
    public TeacherUser Teacher { get; set; } = null!;

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; } = null!;
}