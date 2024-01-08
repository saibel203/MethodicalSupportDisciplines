using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface IMaterialTypeRepository : IRepositoryBase
{
    Task<MaterialTypeRepositoryResponse> GetMaterialTypesAsync();
}