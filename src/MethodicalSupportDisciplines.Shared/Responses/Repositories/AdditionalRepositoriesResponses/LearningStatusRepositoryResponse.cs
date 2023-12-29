using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class LearningStatusRepositoryResponse : BaseResponse
{
    public IReadOnlyList<LearningStatus>? LearningStatuses { get; set; } = new List<LearningStatus>();
}