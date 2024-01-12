using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IDisciplineMaterialService
{
    Task<DisciplineMaterialServiceResponse> RemoveDisciplineMaterialAsync(int disciplineMaterialId);
    Task<DisciplineMaterialServiceResponse> CreateDisciplineMaterialAsync(NewDisciplineMaterialDto? dto);
}