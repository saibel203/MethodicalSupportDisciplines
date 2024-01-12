using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IDisciplineMaterialRepository : IRepositoryBase
{
    Task<DisciplineMaterialRepositoryResponse> RemoveDisciplineMaterialAsync(int disciplineMaterialId);

    Task<DisciplineMaterialRepositoryResponse> CreateDisciplineMaterialAsync(
        DisciplineMaterial disciplineMaterial);
}