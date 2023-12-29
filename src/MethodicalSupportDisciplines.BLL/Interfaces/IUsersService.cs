using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Responses.Services;

namespace MethodicalSupportDisciplines.BLL.Interfaces;

public interface IUsersService
{
    Task<UsersServiceResponse> GetGuestUsersAsync(QueryParameters queryParameters);
    Task<UsersServiceResponse> GetGuestUserByIdAsync(int userId);
    Task<UsersServiceResponse> RemoveGuestUserAsync(int userId);
    Task<UsersServiceResponse> RemoveGuestUserWithoutApplicationUserAsync(int userId);
    Task<UsersServiceResponse> GetTeacherUsersAsync(QueryParameters queryParameters);
    Task<UsersServiceResponse> RemoveTeacherUserAsync(int userId);
    Task<UsersServiceResponse> GetStudentUsersAsync(QueryParameters queryParameters);
    Task<UsersServiceResponse> RemoveStudentUserAsync(int userId);
}