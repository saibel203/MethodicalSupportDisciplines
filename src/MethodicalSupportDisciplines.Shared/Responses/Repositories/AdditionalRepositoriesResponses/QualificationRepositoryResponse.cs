using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class QualificationRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Qualification>? Qualifications { get; set; } = new List<Qualification>();
}