using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class DisciplineMaterialTypeRepositoryResponse : BaseResponse
{
    public IReadOnlyList<DisciplineMaterialType> DisciplineMaterialTypes { get; set; } =
        new List<DisciplineMaterialType>();
}