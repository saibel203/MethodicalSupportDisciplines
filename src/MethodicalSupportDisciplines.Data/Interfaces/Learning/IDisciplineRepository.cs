using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IDisciplineRepository : IRepositoryBase
{
    Task<DisciplineRepositoryResponse> GetAllDisciplinesAsync(string applicationUserId);
    Task<DisciplineRepositoryResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId);
    Task<DisciplineRepositoryResponse> CreateDisciplineAsync(Discipline discipline);
}