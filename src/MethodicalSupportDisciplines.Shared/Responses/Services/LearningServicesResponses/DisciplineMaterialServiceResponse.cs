using MethodicalSupportDisciplines.Shared.Dto.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

public class DisciplineMaterialServiceResponse : BaseResponse
{
    public DisciplineMaterialActionDto DisciplineMaterial { get; set; } = null!;
}