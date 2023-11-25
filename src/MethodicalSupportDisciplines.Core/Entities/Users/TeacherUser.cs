using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Core.Entities.Users;

public class TeacherUser : UserBase
{
    public int TeacherUserId { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;

    public int QualificationId { get; set; }
    public Qualification Qualification { get; set; } = null!;
    
    public ICollection<GroupTeacher> GroupTeachers { get; set; } = new List<GroupTeacher>();
    public ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
}