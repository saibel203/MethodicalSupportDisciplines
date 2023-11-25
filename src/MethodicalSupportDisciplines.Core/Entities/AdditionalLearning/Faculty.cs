using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class Faculty
{
    public int FacultyId { get; set; }
    public string FacultyName { get; set; } = string.Empty;
    public string FacultyShortName { get; set; } = string.Empty;

    public ICollection<StudentUser> Students { get; set; } = new List<StudentUser>();
}