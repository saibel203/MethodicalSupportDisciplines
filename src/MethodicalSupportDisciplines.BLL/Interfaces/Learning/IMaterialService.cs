using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IMaterialService
{
    Task<MaterialServiceResponse> CreateMaterialAsync(NewMaterialDto? dto);
}