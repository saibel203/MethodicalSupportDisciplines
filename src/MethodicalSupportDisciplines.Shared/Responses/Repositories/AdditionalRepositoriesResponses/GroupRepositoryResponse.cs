using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class GroupRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Group>? Groups { get; set; } = new List<Group>();
}