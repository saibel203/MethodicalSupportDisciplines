using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.Learning;

public class Group
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int GroupCourse { get; set; }
    
    public ICollection<StudentUser> StudentUsers { get; set; } = new List<StudentUser>();
    public ICollection<GroupTeacher> GroupTeachers { get; set; } = new List<GroupTeacher>();
    public ICollection<DisciplineGroup> DisciplineGroups { get; set; } = new List<DisciplineGroup>();
}