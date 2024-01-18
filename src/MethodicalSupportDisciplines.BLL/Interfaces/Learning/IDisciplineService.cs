using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IDisciplineService
{
    Task<DisciplineServiceResponse> GetAllDisciplinesAsync(QueryParameters queryParameters, string applicationUserId);
    Task<DisciplineServiceResponse> GetAllDisciplinesForAdminAsync(QueryParameters queryParameters);
    Task<DisciplineServiceResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId);
    Task<DisciplineServiceResponse> GetDisciplineForAdminByIdAsync(int disciplineId);

    Task<DisciplineServiceResponse> GetDisciplinesForStudentAsync(string userId,
        QueryParameters queryParameters);

    Task<DisciplineServiceResponse> GetDisciplineForStudentByIdAsync(int disciplineId, string userId);
    Task<DisciplineServiceResponse> CreateDisciplineAsync(NewDisciplineDto? dto);
    Task<DisciplineServiceResponse> RemoveDisciplineAsync(int disciplineId);
}