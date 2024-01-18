using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IDisciplineRepository : IRepositoryBase
{
    Task<DisciplineRepositoryResponse> GetAllDisciplinesAsync(string applicationUserId);
    Task<DisciplineRepositoryResponse> GetAllDisciplinesForAdminAsync();
    Task<DisciplineRepositoryResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId);
    Task<DisciplineRepositoryResponse> GetDisciplineForAdminByIdAsync(int disciplineId);

    Task<DisciplineRepositoryResponse> GetDisciplineForStudentByIdAsync(int disciplineId,
        string currentUserid);
    Task<DisciplineRepositoryResponse> CreateDisciplineAsync(Discipline discipline);
    Task<DisciplineRepositoryResponse> RemoveDisciplineAsync(int disciplineId);
    Task<DisciplineRepositoryResponse> GetDisciplinesForStudentGroup(string userId);
}