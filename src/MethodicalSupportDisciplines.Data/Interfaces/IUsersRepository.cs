using MethodicalSupportDisciplines.Shared.Responses.Repositories;

namespace MethodicalSupportDisciplines.Data.Interfaces;

public interface IUsersRepository : IRepositoryBase
{
    Task<UsersRepositoryResponse> GetGuestUsersAsync();
    Task<UsersRepositoryResponse> GetGuestUserByIdAsync(int userId);
    Task<UsersRepositoryResponse> RemoveGuestUserAsync(int userId);
    Task<UsersRepositoryResponse> RemoveGuestUserWithoutApplicationUserAsync(int userId);
    Task<UsersRepositoryResponse> GetTeacherUsersAsync();
    Task<UsersRepositoryResponse> GetTeacherUserByApplicationUserIdAsync(string userId);
    Task<UsersRepositoryResponse> RemoveTeacherUserAsync(int userId);
    Task<UsersRepositoryResponse> GetStudentUsersAsync();
    Task<UsersRepositoryResponse> RemoveStudentUserAsync(int userId);
}