using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Learning;

public interface IMaterialDisciplineMaterialRepository : IRepositoryBase
{
    Task<MaterialDisciplineMaterialRepositoryResponse> CreateMaterialDisciplineMaterialAsync(
        MaterialDisciplineMaterial materialDisciplineMaterial);
}