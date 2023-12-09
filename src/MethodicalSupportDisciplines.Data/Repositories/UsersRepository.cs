using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;

namespace MethodicalSupportDisciplines.Data.Repositories;

public class UsersRepository : RepositoryBase, IUsersRepository
{
    private readonly ILogger<UsersRepository> _logger;
    private readonly IStringLocalizer<UsersRepository> _stringLocalization;

    public UsersRepository(DataDbContext context, ILogger<UsersRepository> logger,
        IStringLocalizer<UsersRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<UsersRepositoryResponse> GetGuestUsersAsync()
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve guest users from the database.");

            return new UsersRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve guest users from the database",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersRepositoryResponse> GetGuestUserByIdAsync(int userId)
    {
        try
        {
            GuestUser? guestUser = await Context.GuestUsers
                .Include(guestUserData => guestUserData.ApplicationUser)
                .FirstOrDefaultAsync(guestUserData => guestUserData.GuestUserId == userId);

            if (guestUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            return new UsersRepositoryResponse
            {
                Message = _stringLocalization["GetUserSuccess"],
                IsSuccess = true,
                GuestUser = guestUser
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve a user.");

            return new UsersRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve a user",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersRepositoryResponse> RemoveGuestUserAsync(int userId)
    {
        try
        {
            GuestUser? guestUser = await Context.GuestUsers
                .FirstOrDefaultAsync(guestUserData => guestUserData.GuestUserId == userId);

            if (guestUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            Context.GuestUsers.Remove(guestUser);
            await Context.SaveChangesAsync();

            return new UsersRepositoryResponse
            {
                Message = _stringLocalization["RemoveUserSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a user from the database.");

            return new UsersRepositoryResponse
            {
                Message = _stringLocalization["UnknownError"],
                IsSuccess = false
            };
        }
    }
}