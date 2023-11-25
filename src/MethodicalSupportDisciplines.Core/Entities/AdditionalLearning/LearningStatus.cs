using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;

public class LearningStatus
{
    public int LearningStatusId { get; set; }
    public string LearningStatusName { get; set; } = string.Empty;

    public ICollection<StudentUser> Students { get; set; } = new List<StudentUser>();
}