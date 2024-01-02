using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Core.Entities.Users;

public class StudentUser : UserBase
{
    public int StudentUserId { get; set; }

    public int FormatLearningId { get; set; }
    public FormatLearning FormatLearning { get; set; } = null!;
    
    public int LearningStatusId { get; set; }
    public LearningStatus LearningStatus { get; set; } = null!;
    
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    
    public int SpecialtyId { get; set; }
    public Speciality Speciality { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public ICollection<Mark> Marks { get; set; } = new List<Mark>();
}