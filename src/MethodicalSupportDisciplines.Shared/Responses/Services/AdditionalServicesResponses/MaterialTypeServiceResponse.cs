using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class MaterialTypeServiceResponse : BaseResponse
{
    public IReadOnlyList<MaterialTypeDto> MaterialTypes { get; set; } = new List<MaterialTypeDto>();
}