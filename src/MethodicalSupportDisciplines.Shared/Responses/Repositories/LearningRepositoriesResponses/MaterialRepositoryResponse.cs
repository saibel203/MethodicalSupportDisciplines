using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

public class MaterialRepositoryResponse : BaseResponse
{
    public int CreatedMaterialId { get; set; }
    public Material Material { get; set; } = null!;
}