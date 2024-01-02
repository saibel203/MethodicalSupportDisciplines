using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IDisciplineService
{
    Task<DisciplineServiceResponse> GetAllDisciplinesAsync(QueryParameters queryParameters, string applicationUserId);
    Task<DisciplineServiceResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId);
    Task<DisciplineServiceResponse> CreateDisciplineAsync(NewDisciplineDto? dto);
}