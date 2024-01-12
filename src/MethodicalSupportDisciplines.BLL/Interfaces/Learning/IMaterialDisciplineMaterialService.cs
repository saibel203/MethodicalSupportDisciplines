using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Learning;

public interface IMaterialDisciplineMaterialService
{
    Task<MaterialDisciplineMaterialServiceResponse> CreateMaterialDisciplineMaterialAsync(
        NewMaterialDisciplineMaterialDto? dto);
}