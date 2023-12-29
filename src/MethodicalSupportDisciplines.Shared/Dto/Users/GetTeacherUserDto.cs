namespace MethodicalSupportDisciplines.Shared.Dto.Users;

public class GetTeacherUserDto : BaseUserDto
{
    public int TeacherUserId { get; set; }
    public string QualificationName { get; set; } = string.Empty;
}