using MethodicalSupportDisciplines.Shared.Responses.Repositories;

namespace MethodicalSupportDisciplines.Data.Interfaces;

public interface IUsersRepository : IRepositoryBase
{
    Task<UsersRepositoryResponse> GetGuestUsersAsync();
    Task<UsersRepositoryResponse> GetGuestUserByIdAsync(int userId);
    Task<UsersRepositoryResponse> RemoveGuestUserAsync(int userId);
}