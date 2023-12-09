using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Responses.Services;

namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IUsersService
{
    Task<UsersServiceResponse> GetGuestUsersAsync(QueryParameters queryParameters);
    Task<UsersServiceResponse> GetGuestUserByIdAsync(int userId);
    Task<UsersServiceResponse> RemoveGuestUserAsync(int userId);
}