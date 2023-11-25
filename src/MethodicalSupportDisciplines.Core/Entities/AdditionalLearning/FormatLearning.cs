using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class FormatLearning
{
    public int FormatLearningId { get; set; }
    public string FormatLearningName { get; set; } = string.Empty;

    public ICollection<StudentUser> Students { get; set; } = new List<StudentUser>();
}