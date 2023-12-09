using MethodicalSupportDisciplines.Core.Entities.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Repositories;

public class UsersRepositoryResponse : BaseResponse
{
    public IReadOnlyList<GuestUser>? GuestUsers { get; set; } = new List<GuestUser>();
    public GuestUser? GuestUser { get; set; }
}