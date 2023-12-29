using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface IFacultyRepository : IRepositoryBase
{
    Task<FacultyRepositoryResponse> GetAllFacultiesAsync();
}