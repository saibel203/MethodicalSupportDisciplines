using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories;

public class UsersRepositoryResponse : BaseResponse
{
    public IReadOnlyList<GuestUser>? GuestUsers { get; set; } = new List<GuestUser>();
    public IReadOnlyList<TeacherUser>? TeacherUsers { get; set; } = new List<TeacherUser>();
    public IReadOnlyList<StudentUser>? StudentUsers { get; set; } = new List<StudentUser>();
    public GuestUser? GuestUser { get; set; }
    public int TeacherUserId { get; set; }

    public StudentUser? StudentUser { get; set; }
    public TeacherUser? TeacherUser { get; set; }
    public AdminUser? AdminUser { get; set; }
}