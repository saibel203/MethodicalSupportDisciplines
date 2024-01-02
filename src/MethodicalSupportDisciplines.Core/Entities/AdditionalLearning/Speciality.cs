using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class Speciality
{
    public int SpecialityId { get; set; }
    public string SpecialityName { get; set; } = string.Empty;
    public int SpecialityNumber { get; set; }
    
    public ICollection<StudentUser> Students { get; set; } = new List<StudentUser>();
}