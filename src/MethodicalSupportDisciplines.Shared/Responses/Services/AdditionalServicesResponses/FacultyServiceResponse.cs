using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;

public class FacultyServiceResponse : BaseResponse
{
    public IReadOnlyList<FacultyDto> Faculties { get; set; } = new List<FacultyDto>();
}