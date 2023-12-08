using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class Specialty
{
    public int SpecialtyId { get; set; }
    public string SpecialtyName { get; set; } = string.Empty;
    public int SpecialityNumber { get; set; }
    
    public ICollection<StudentUser> Students { get; set; } = new List<StudentUser>();
}