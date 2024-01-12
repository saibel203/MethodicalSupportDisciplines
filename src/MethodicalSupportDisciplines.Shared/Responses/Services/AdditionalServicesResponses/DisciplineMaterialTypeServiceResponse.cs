using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class DisciplineMaterialTypeServiceResponse : BaseResponse
{
    public IReadOnlyList<DisciplineMaterialTypeDto> DisciplineMaterials { get; set; } =
        new List<DisciplineMaterialTypeDto>();
}