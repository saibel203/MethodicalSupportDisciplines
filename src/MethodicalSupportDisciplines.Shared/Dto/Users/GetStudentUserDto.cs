namespace MethodicalSupportDisciplines.Shared.Dto.Users;

public class GetStudentUserDto : BaseUserDto
{
    public int StudentUserId { get; set; }
    public string FormatLearningName { get; set; } = string.Empty;
    public string LearningStatusName { get; set; } = string.Empty;
    public string FacultyName { get; set; } = string.Empty;
    public string FacultyShortName { get; set; } = string.Empty;
    public string SpecialtyName { get; set; } = string.Empty;
    public int SpecialityNumber { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public int GroupCourse { get; set; }
}