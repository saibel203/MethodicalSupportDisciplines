using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class MaterialTypeRepositoryResponse : BaseResponse
{
    public IReadOnlyList<MaterialType> MaterialTypes { get; set; } = new List<MaterialType>();
}