using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Core.Models.Identity;
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
            IReadOnlyList<GuestUser> guestUsers = await Context.Set<GuestUser>()
                .Include(guestUser => guestUser.ApplicationUser)
                .OrderByDescending(guestUser => guestUser.GuestUserId)
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
            GuestUser? guestUser = await Context.Set<GuestUser>()
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
            GuestUser? guestUser = await Context.Set<GuestUser>()
                .FirstOrDefaultAsync(guestUserData => guestUserData.GuestUserId == userId);

            if (guestUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            ApplicationUser? applicationUser = await Context.Set<ApplicationUser>()
                .FirstOrDefaultAsync(userData => userData.GuestUser == guestUser);
            
            if (applicationUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            Context.Users.Remove(applicationUser);
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
    
    public async Task<UsersRepositoryResponse> RemoveGuestUserWithoutApplicationUserAsync(int userId)
    {
        try
        {
            GuestUser? guestUser = await Context.Set<GuestUser>()
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

    public async Task<UsersRepositoryResponse> GetTeacherUsersAsync()
    {
        try
        {
            IReadOnlyList<TeacherUser> teacherUsers = await Context.Set<TeacherUser>()
                .Include(teacherUserData => teacherUserData.ApplicationUser)
                .Include(teacherUserData => teacherUserData.Qualification)
                .OrderByDescending(teacherUserData => teacherUserData.TeacherUserId)
                .ToListAsync();

            return new UsersRepositoryResponse
            {
                Message = "Teachers successfully received",
                IsSuccess = true,
                TeacherUsers = teacherUsers
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve teachers from the database.");

            return new UsersRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve teachers from the database",
                IsSuccess = false
            };
        }
    }
    
    public async Task<int> GetTeacherUserByApplicationUserIdAsync(string userId)
    {
        try
        {
            var test = await Context.Set<TeacherUser>()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            return test!.TeacherUserId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return 0;
        }
    }

    public async Task<UsersRepositoryResponse> RemoveTeacherUserAsync(int userId)
    {
        try
        {
            TeacherUser? teacherUser = await Context.Set<TeacherUser>()
                .FirstOrDefaultAsync(teacherUserData => teacherUserData.TeacherUserId == userId);

            if (teacherUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            ApplicationUser? applicationUser = await Context.Set<ApplicationUser>()
                .FirstOrDefaultAsync(userData => userData.TeacherUser == teacherUser);
            
            if (applicationUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            Context.Users.Remove(applicationUser);
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
    
    public async Task<UsersRepositoryResponse> GetStudentUsersAsync()
    {
        try
        {
            IReadOnlyList<StudentUser> studentUsers = await Context.Set<StudentUser>()
                .Include(studentUserData => studentUserData.ApplicationUser)
                .Include(studentUserData => studentUserData.FormatLearning)
                .Include(studentUserData => studentUserData.LearningStatus)
                .Include(studentUserData => studentUserData.Faculty)
                .Include(studentUserData => studentUserData.Speciality)
                .Include(studentUserData => studentUserData.Group)
                .OrderByDescending(studentUserData => studentUserData.StudentUserId)
                .ToListAsync();

            return new UsersRepositoryResponse
            {
                Message = "Students successfully received",
                IsSuccess = true,
                StudentUsers = studentUsers
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve students from the database.");

            return new UsersRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve students from the database",
                IsSuccess = false
            };
        }
    }
    
    public async Task<UsersRepositoryResponse> RemoveStudentUserAsync(int userId)
    {
        try
        {
            StudentUser? studentUser = await Context.Set<StudentUser>()
                .FirstOrDefaultAsync(studentUserData => studentUserData.StudentUserId == userId);

            if (studentUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            ApplicationUser? applicationUser = await Context.Set<ApplicationUser>()
                .FirstOrDefaultAsync(userData => userData.StudentUser == studentUser);
            
            if (applicationUser is null)
            {
                return new UsersRepositoryResponse
                {
                    Message = _stringLocalization["UserNotFound"],
                    IsSuccess = false
                };
            }

            Context.Users.Remove(applicationUser);
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