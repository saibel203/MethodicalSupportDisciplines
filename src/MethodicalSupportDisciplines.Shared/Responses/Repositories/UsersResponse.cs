using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories;

public class UsersResponse : BaseResponse
{
    public IReadOnlyList<GuestUser>? GuestUsers { get; set; } = new List<GuestUser>();
}