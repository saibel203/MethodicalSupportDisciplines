using MethodicalSupportDisciplines.Shared.Dto.Users;

namespace MethodicalSupportDisciplines.Shared.Responses.Services;

public class UsersServiceResponse : ListBaseResponse
{
    public IReadOnlyList<GetGuestUserDto>? GuestUsers { get; set; } = new List<GetGuestUserDto>();
    public GetGuestUserDto? GuestUser { get; set; }
}