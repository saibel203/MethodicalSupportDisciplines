using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IDisciplineGroupService
{
    Task<DisciplineGroupServiceResponse> RemoveDisciplineGroupAsync(int disciplineId, int groupId);
    Task<DisciplineGroupServiceResponse> CreateDisciplineGroupAsync(NewDisciplineGroupDto? dto);
}