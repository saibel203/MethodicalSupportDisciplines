using MethodicalSupportDisciplines.Shared.Responses.Repositories;

namespace MethodicalSupportDisciplines.Data.Interfaces;

public interface IUsersRepository : IRepositoryBase
{
    Task<UsersResponse> GetGuestUsersAsync();
}