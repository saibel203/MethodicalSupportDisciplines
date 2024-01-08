using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IMaterialRepository : IRepositoryBase
{
    Task<MaterialRepositoryResponse> CreateMaterialAsync(Material material);
}