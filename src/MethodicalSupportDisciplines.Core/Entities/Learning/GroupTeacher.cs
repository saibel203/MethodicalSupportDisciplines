using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class GroupTeacher
{
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public TeacherUser TeacherUser { get; set; } = null!;
    public Group Group { get; set; } = null!;
}