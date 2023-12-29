using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface IGroupRepository : IRepositoryBase
{
    Task<GroupRepositoryResponse> GetAllGroupsAsync();
}