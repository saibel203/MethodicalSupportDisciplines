using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class SpecialityServiceResponse : BaseResponse
{
    public IReadOnlyList<SpecialityDto> Specialties { get; set; } = new List<SpecialityDto>();
}