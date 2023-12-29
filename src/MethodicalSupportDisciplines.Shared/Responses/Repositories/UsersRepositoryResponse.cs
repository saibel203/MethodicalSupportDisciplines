using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories;

public class UsersRepositoryResponse : BaseResponse
{
    public IReadOnlyList<GuestUser>? GuestUsers { get; set; } = new List<GuestUser>();
    public IReadOnlyList<TeacherUser>? TeacherUsers { get; set; } = new List<TeacherUser>();
    public IReadOnlyList<StudentUser>? StudentUsers { get; set; } = new List<StudentUser>();
    public GuestUser? GuestUser { get; set; }
}