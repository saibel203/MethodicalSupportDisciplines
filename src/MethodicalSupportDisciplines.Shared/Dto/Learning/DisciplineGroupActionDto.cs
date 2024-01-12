using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Dto.Learning;

public class DisciplineGroupActionDto
{
    public int DisciplineId { get; set; }
    public int GroupId { get; set; }
    public GroupDto Group { get; set; } = null!;
}