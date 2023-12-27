using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class SpecialityRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Specialty>? Specialties { get; set; } = new List<Specialty>();
}