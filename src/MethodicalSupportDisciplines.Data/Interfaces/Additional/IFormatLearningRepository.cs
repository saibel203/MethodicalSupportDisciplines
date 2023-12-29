using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface IFormatLearningRepository : IRepositoryBase
{
    Task<FormatLearningRepositoryResponse> GetAllFormatLearningsAsync();
}