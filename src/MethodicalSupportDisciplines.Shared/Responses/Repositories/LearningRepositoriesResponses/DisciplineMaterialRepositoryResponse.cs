using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

public class DisciplineMaterialRepositoryResponse : BaseResponse
{
    public DisciplineMaterial DisciplineMaterial { get; set; } = null!;
}