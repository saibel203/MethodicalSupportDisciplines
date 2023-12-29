using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class FormatLearningRepositoryResponse : BaseResponse
{
    public IReadOnlyList<FormatLearning>? FormatLearnings { get; set; } = new List<FormatLearning>();
}