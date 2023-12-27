using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface ISpecialityRepository : IRepositoryBase
{
    Task<SpecialityRepositoryResponse> GetAllSpecialitiesAsync();
}