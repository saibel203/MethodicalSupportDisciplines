using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class FormatLearningServiceResponse : BaseResponse
{
    public IReadOnlyList<FormatLearningDto> FormatLearnings { get; set; } = new List<FormatLearningDto>();
}