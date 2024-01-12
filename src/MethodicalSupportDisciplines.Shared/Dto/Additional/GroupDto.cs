using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Dto.Additional;

public class GroupDto
{
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int GroupCourse { get; set; }
    public ICollection<StudentUser> StudentUsers { get; set; } = new List<StudentUser>();
}