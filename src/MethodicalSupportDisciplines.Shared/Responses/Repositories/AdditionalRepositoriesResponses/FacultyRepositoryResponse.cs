using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class FacultyRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Faculty>? Faculties { get; set; } = new List<Faculty>();
}