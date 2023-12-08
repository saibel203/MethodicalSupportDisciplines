using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MethodicalSupportDisciplines.Data.Repositories;

public class UsersRepository : RepositoryBase, IUsersRepository
{
    public UsersRepository(DataDbContext context) : base(context)
    {
    }

    public async Task<UsersResponse> GetGuestUsersAsync()
    {
        IReadOnlyList<GuestUser> guestUsers = await Context.GuestUsers
            .Include(guestUser => guestUser.ApplicationUser)
            .ToListAsync();

        return new()
        {
            Message = "Guest Users received successfully",
            IsSuccess = true,
            GuestUsers = guestUsers
        };
    }
}