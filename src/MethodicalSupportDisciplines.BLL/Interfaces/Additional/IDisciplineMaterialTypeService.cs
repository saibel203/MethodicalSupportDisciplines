using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

namespace MethodicalSupportDisciplines.BLL.Interfaces.Additional;

public interface IDisciplineMaterialTypeService
{
    Task<DisciplineMaterialTypeServiceResponse> GetDisciplineMaterialTypesAsync();
}