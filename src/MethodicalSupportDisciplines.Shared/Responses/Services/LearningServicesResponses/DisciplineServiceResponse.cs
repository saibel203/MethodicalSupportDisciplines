using MethodicalSupportDisciplines.Shared.Dto.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

public class DisciplineServiceResponse : ListBaseResponse
{
    public IReadOnlyList<DisciplineActionDto> Disciplines { get; set; } = new List<DisciplineActionDto>();
    public IReadOnlyList<DisciplineGroupActionDto> DisciplineGroups { get; set; } = new List<DisciplineGroupActionDto>();
    public DisciplineActionDto Discipline { get; set; } = null!;
}