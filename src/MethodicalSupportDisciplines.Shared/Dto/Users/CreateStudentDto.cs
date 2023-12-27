namespace MethodicalSupportDisciplines.Shared.Dto.Users;

public class CreateStudentDto
{
    public int StudentUserId { get; set; }
    public int FormatLearningId { get; set; }
    public int LearningStatusId { get; set; }
    public int FacultyId { get; set; }
    public int SpecialtyId { get; set; }
    public int GroupId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
}