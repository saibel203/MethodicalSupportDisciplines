using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class QualificationServiceResponse : BaseResponse
{
    public IReadOnlyList<QualificationDto> Qualifications { get; set; } = new List<QualificationDto>();
}