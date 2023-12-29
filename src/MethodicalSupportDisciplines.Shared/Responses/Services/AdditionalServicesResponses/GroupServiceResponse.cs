using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class GroupServiceResponse : BaseResponse
{
    public IReadOnlyList<GroupDto> Groups { get; set; } = new List<GroupDto>();
}