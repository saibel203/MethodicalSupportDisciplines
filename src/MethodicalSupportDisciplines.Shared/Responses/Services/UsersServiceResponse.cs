using MethodicalSupportDisciplines.Shared.Dto.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Services;

public class UsersServiceResponse : ListBaseResponse
{
    public IReadOnlyList<GetGuestUserDto>? GuestUsers { get; set; } = new List<GetGuestUserDto>();
    public IReadOnlyList<GetTeacherUserDto>? TeacherUsers { get; set; } = new List<GetTeacherUserDto>();
    public IReadOnlyList<GetStudentUserDto>? StudentUsers { get; set; } = new List<GetStudentUserDto>();
    public GetGuestUserDto? GuestUser { get; set; }
    public int TeacherUserId { get; set; }
}