using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

public class SpecialityRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Speciality>? Specialties { get; set; } = new List<Speciality>();
}