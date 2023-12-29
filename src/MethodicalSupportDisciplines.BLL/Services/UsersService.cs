using AutoMapper;
using MethodicalSupportDisciplines.BLL.Helpers;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.Responses.Repositories;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services;

public class UsersService : BaseService<IUsersRepository>, IUsersService
{
    private readonly ILogger<UsersService> _logger;

    public UsersService(IUsersRepository repository, IMapper mapper, ILogger<UsersService> logger) : base(repository,
        mapper)
    {
        _logger = logger;
    }

    public async Task<UsersServiceResponse> GetGuestUsersAsync(QueryParameters queryParameters)
    {
        try
        {
            UsersRepositoryResponse guestUsersResponse = await _repository.GetGuestUsersAsync();

            if (!guestUsersResponse.IsSuccess || guestUsersResponse.GuestUsers is null)
            {
                return new UsersServiceResponse
                {
                    Message = guestUsersResponse.Message,
                    IsSuccess = false
                };
            }

            int skipAmount = PagesParameters.GuestUsersTablePageCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<GuestUser> filteredGuestUsers = guestUsersResponse.GuestUsers;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                filteredGuestUsers = filteredGuestUsers
                    .Where(guestUserData =>
                        guestUserData.ApplicationUser != null &&
                        (guestUserData.ApplicationUser.UserName!.Contains(queryParameters.SearchString!) ||
                         guestUserData.FirstName.Contains(queryParameters.SearchString) ||
                         guestUserData.LastName.Contains(queryParameters.SearchString) ||
                         guestUserData.Patronymic.Contains(queryParameters.SearchString)))
                    .ToList();
            }

            int guestUsersCount = filteredGuestUsers.Count;
            int pageCount = (int)Math.Ceiling((double)guestUsersCount / PagesParameters.GuestUsersTablePageCount);

            IEnumerable<GetGuestUserDto> guestUserDto = _mapper.Map<IEnumerable<GetGuestUserDto>>(filteredGuestUsers);

            return new UsersServiceResponse
            {
                Message = guestUsersResponse.Message,
                IsSuccess = true,
                GuestUsers = guestUserDto
                    .Skip(skipAmount)
                    .Take(PagesParameters.GuestUsersTablePageCount)
                    .ToList(),
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                SearchString = queryParameters.SearchString,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to get guest users.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to get guest users",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> GetGuestUserByIdAsync(int userId)
    {
        try
        {
            UsersRepositoryResponse getUsersResult = await _repository.GetGuestUserByIdAsync(userId);

            if (!getUsersResult.IsSuccess || getUsersResult.GuestUser is null)
            {
                return new UsersServiceResponse
                {
                    Message = getUsersResult.Message,
                    IsSuccess = false
                };
            }

            GetGuestUserDto guestUserDto = _mapper.Map<GetGuestUserDto>(getUsersResult.GuestUser);

            return new UsersServiceResponse
            {
                Message = getUsersResult.Message,
                IsSuccess = true,
                GuestUser = guestUserDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve a user.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve a user",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> RemoveGuestUserAsync(int userId)
    {
        try
        {
            UsersRepositoryResponse removeResult = await _repository.RemoveGuestUserAsync(userId);

            if (!removeResult.IsSuccess)
            {
                return new UsersServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            return new UsersServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a user.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to delete a user",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> RemoveGuestUserWithoutApplicationUserAsync(int userId)
    {
        try
        {
            UsersRepositoryResponse removeResult =
                await _repository.RemoveGuestUserWithoutApplicationUserAsync(userId);

            if (!removeResult.IsSuccess)
            {
                return new UsersServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            return new UsersServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a user.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to delete a user",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> GetTeacherUsersAsync(QueryParameters queryParameters)
    {
        try
        {
            UsersRepositoryResponse getUsersResult = await _repository.GetTeacherUsersAsync();

            if (!getUsersResult.IsSuccess || getUsersResult.TeacherUsers is null)
            {
                return new UsersServiceResponse
                {
                    Message = getUsersResult.Message,
                    IsSuccess = false
                };
            }

            int skipAmount = PagesParameters.GuestUsersTablePageCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<TeacherUser> filteredTeacherUsers = getUsersResult.TeacherUsers;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                filteredTeacherUsers = filteredTeacherUsers
                    .Where(guestUserData =>
                        guestUserData.ApplicationUser != null &&
                        (guestUserData.ApplicationUser.UserName!.Contains(queryParameters.SearchString!) ||
                         guestUserData.FirstName.Contains(queryParameters.SearchString) ||
                         guestUserData.LastName.Contains(queryParameters.SearchString) ||
                         guestUserData.Patronymic.Contains(queryParameters.SearchString)))
                    .ToList();
            }

            int guestUsersCount = filteredTeacherUsers.Count;
            int pageCount = (int)Math.Ceiling((double)guestUsersCount / PagesParameters.GuestUsersTablePageCount);

            IReadOnlyList<GetTeacherUserDto> teacherUsersDto = _mapper.Map<IReadOnlyList<GetTeacherUserDto>>(
                filteredTeacherUsers);

            return new UsersServiceResponse
            {
                Message = getUsersResult.Message,
                IsSuccess = true,
                TeacherUsers = teacherUsersDto
                    .Skip(skipAmount)
                    .Take(PagesParameters.GuestUsersTablePageCount)
                    .ToList(),
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                SearchString = queryParameters.SearchString,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to get teacher users.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to get teacher users",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> RemoveTeacherUserAsync(int userId)
    {
        try
        {
            UsersRepositoryResponse removeResult = await _repository.RemoveTeacherUserAsync(userId);

            if (!removeResult.IsSuccess)
            {
                return new UsersServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            return new UsersServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a user.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to delete a user",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> GetStudentUsersAsync(QueryParameters queryParameters)
    {
        try
        {
            UsersRepositoryResponse getUsersResult = await _repository.GetStudentUsersAsync();

            if (!getUsersResult.IsSuccess || getUsersResult.StudentUsers is null)
            {
                return new UsersServiceResponse
                {
                    Message = getUsersResult.Message,
                    IsSuccess = false
                };
            }

            int skipAmount = PagesParameters.GuestStudentUsersTablePageCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<StudentUser> filteredStudentUsers = getUsersResult.StudentUsers;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                filteredStudentUsers = filteredStudentUsers
                    .Where(guestUserData =>
                        guestUserData.ApplicationUser != null &&
                        (guestUserData.ApplicationUser.UserName!.Contains(queryParameters.SearchString!) ||
                         guestUserData.FirstName.Contains(queryParameters.SearchString) ||
                         guestUserData.LastName.Contains(queryParameters.SearchString) ||
                         guestUserData.Patronymic.Contains(queryParameters.SearchString)))
                    .ToList();
            }

            int guestUsersCount = filteredStudentUsers.Count;
            int pageCount =
                (int)Math.Ceiling((double)guestUsersCount / PagesParameters.GuestStudentUsersTablePageCount);

            IReadOnlyList<GetStudentUserDto> studentUsersDto = _mapper.Map<IReadOnlyList<GetStudentUserDto>>(
                filteredStudentUsers);

            return new UsersServiceResponse
            {
                Message = getUsersResult.Message,
                IsSuccess = true,
                StudentUsers = studentUsersDto
                    .Skip(skipAmount)
                    .Take(PagesParameters.GuestStudentUsersTablePageCount)
                    .ToList(),
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                SearchString = queryParameters.SearchString,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to get student users.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to get student users",
                IsSuccess = false
            };
        }
    }

    public async Task<UsersServiceResponse> RemoveStudentUserAsync(int userId)
    {
        try
        {
            UsersRepositoryResponse removeResult = await _repository.RemoveStudentUserAsync(userId);

            if (!removeResult.IsSuccess)
            {
                return new UsersServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            return new UsersServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a user.");

            return new UsersServiceResponse
            {
                Message = "An unknown error occurred while trying to delete a user",
                IsSuccess = false
            };
        }
    }
}