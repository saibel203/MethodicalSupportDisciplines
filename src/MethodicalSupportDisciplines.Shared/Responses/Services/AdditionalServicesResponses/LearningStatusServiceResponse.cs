using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class LearningStatusServiceResponse : BaseResponse
{
    public IReadOnlyList<LearningStatusDto> LearningStatuses { get; set; } = new List<LearningStatusDto>();
}