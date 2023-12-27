namespace MethodicalSupportDisciplines.Shared.Dto.Users;

public class CreateTeacherDto
{
    public int TeacherUserId { get; set; }
    public int QualificationId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
}