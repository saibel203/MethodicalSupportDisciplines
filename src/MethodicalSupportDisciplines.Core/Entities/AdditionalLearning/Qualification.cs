using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class Qualification
{
    public int QualificationId { get; set; }
    public string QualificationName { get; set; } = string.Empty;

    public ICollection<TeacherUser> Teachers { get; set; } = new List<TeacherUser>();
}