using MethodicalSupportDisciplines.Core.Entities.Learning;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;

public class DisciplineRepositoryResponse : BaseResponse
{
    public IReadOnlyList<Discipline> Disciplines { get; set; } = new List<Discipline>();
    public Discipline? Discipline { get; set; }
}