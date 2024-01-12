using MethodicalSupportDisciplines.Shared.Dto.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;

public class MaterialServiceResponse : BaseResponse
{
    public int CreatedMaterialId { get; set; }
    public NewMaterialDto Material { get; set; } = null!;
}