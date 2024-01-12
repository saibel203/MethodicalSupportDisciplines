using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface IDisciplineMaterialTypeRepository : IRepositoryBase
{
    Task<DisciplineMaterialTypeRepositoryResponse> GetDisciplineMaterialTypesAsync();
}