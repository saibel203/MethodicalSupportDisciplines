using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class DisciplineActionDto
{
    public int DisciplineId { get; set; }
    public string DisciplineName { get; set; } = string.Empty;
    public string DisciplineDescription { get; set; } = string.Empty;
    public DateTime DisciplineCreatedDate { get; set; } = DateTime.Now;
    public TeacherUser Teacher { get; set; } = null!;
    public ICollection<DisciplineMaterialActionDto> DisciplineMaterials { get; set; } = new List<DisciplineMaterialActionDto>();
    public ICollection<DisciplineGroupActionDto> DisciplineGroups { get; set; } = new List<DisciplineGroupActionDto>();
}