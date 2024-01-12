using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IDisciplineGroupRepository : IRepositoryBase
{
    Task<DisciplineGroupRepositoryResponse> RemoveDisciplineGroupAsync(int disciplineId, int groupId);
    Task<DisciplineGroupRepositoryResponse> CreateDisciplineGroupAsync(DisciplineGroup disciplineGroup);
}