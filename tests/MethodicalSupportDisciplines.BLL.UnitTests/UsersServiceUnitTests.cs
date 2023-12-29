using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Services;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Data.Interfaces;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.Responses.Repositories;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MethodicalSupportDisciplines.BLL.UnitTests;

public class UsersServiceUnitTests
{
    private readonly IUsersService _usersService;
    private readonly Mock<IUsersRepository> _usersRepositoryMock;

    public UsersServiceUnitTests()
    {
        _usersRepositoryMock = new Mock<IUsersRepository>();

        Mock<ILogger<UsersService>> loggerMock = new Mock<ILogger<UsersService>>();
        IMapper mapperMock = MapperMock.GetMapper();

        _usersService = new UsersService(_usersRepositoryMock.Object,
            mapperMock, loggerMock.Object);
    }

    [Fact]
    public async Task GetGuestUsersAsync_NotEmptyGuestUsersList_ReturnSuccessResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        IReadOnlyList<GuestUser> expectedUsers = new List<GuestUser>
        {
            It.IsAny<GuestUser>(),
            It.IsAny<GuestUser>()
        };

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            GuestUsers = expectedUsers
        };

        _usersRepositoryMock.Setup(repository => repository.GetGuestUsersAsync())
            .ReturnsAsync(expectedResponse);

        int expectedUsersCount = expectedUsers.Count;

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetGuestUsersAsync(queryParameters);
        int expectedResultCount = resultResponse.GuestUsers!.Count;

        // Assert
        Assert.Equal(expectedUsersCount, expectedResultCount);
        Assert.True(resultResponse.IsSuccess);
        Assert.NotEmpty(resultResponse.GuestUsers);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Null(resultResponse.GuestUser!);
        Assert.NotNull(resultResponse.GuestUsers);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetGuestUserDto>>(resultResponse.GuestUsers);
    }

    [Fact]
    public async Task GetGuestUsersAsync_EmptyGuestUsersListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.GetGuestUsersAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetGuestUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.NotNull(resultResponse.GuestUsers);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Null(resultResponse.GuestUser!);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetGuestUserDto>>(resultResponse.GuestUsers);
    }

    [Fact]
    public async Task GetGuestUsersAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        _usersRepositoryMock.Setup(repository => repository.GetGuestUsersAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to get guest users";

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetGuestUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.NotNull(resultResponse.GuestUsers);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Null(resultResponse.GuestUser!);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetGuestUserDto>>(resultResponse.GuestUsers);
        Assert.Contains(expectedErrorMessage, resultResponse.Message);
    }

    [Fact]
    public async Task GetGuestUserByIdAsync_NotEmptyGuestUser_ReturnSuccessResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            GuestUser = new GuestUser()
        };

        _usersRepositoryMock.Setup(repository => repository.GetGuestUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse serviceResponse = await _usersService.GetGuestUserByIdAsync(It.IsAny<int>());

        // Assert
        Assert.True(serviceResponse.IsSuccess);
        Assert.NotNull(serviceResponse.GuestUser);
        Assert.Empty(serviceResponse.StudentUsers!);
        Assert.Empty(serviceResponse.TeacherUsers!);
        Assert.Empty(serviceResponse.GuestUsers!);
        Assert.IsAssignableFrom<UsersServiceResponse>(serviceResponse);
        Assert.IsAssignableFrom<GetGuestUserDto>(serviceResponse.GuestUser);
    }

    [Fact]
    public async Task GetGuestUserByIdAsync_NullGuestUser_ReturnErrorResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
            GuestUser = null
        };

        _usersRepositoryMock.Setup(repository => repository.GetGuestUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse serviceResponse = await _usersService.GetGuestUserByIdAsync(It.IsAny<int>());

        // Assert
        Assert.False(serviceResponse.IsSuccess);
        Assert.Null(serviceResponse.GuestUser);
        Assert.Empty(serviceResponse.StudentUsers!);
        Assert.Empty(serviceResponse.TeacherUsers!);
        Assert.Empty(serviceResponse.GuestUsers!);
        Assert.IsAssignableFrom<UsersServiceResponse>(serviceResponse);
    }

    [Fact]
    public async Task GetGuestUserByIdAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _usersRepositoryMock.Setup(repository => repository.GetGuestUserByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a user";

        // Act
        UsersServiceResponse serviceResponse = await _usersService.GetGuestUserByIdAsync(It.IsAny<int>());

        // Assert
        Assert.False(serviceResponse.IsSuccess);
        Assert.Null(serviceResponse.GuestUser);
        Assert.Empty(serviceResponse.StudentUsers!);
        Assert.Empty(serviceResponse.TeacherUsers!);
        Assert.Empty(serviceResponse.GuestUsers!);
        Assert.Contains(expectedErrorMessage, serviceResponse.Message);
    }

    [Fact]
    public async Task RemoveGuestUserAsync_ExistUser_ReturnSuccessResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveGuestUserAsync(It.IsAny<int>());

        // Assert
        Assert.True(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveGuestUserAsync_NonExistingUser_ReturnErrorResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveGuestUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveGuestUserAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to delete a user";

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveGuestUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Equal(expectedErrorMessage, resultResponse.Message);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveGuestUserWithoutApplicationUserAsync_ExistUser_ReturnSuccessResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse =
            await _usersService.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>());

        // Assert
        Assert.True(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveGuestUserWithoutApplicationUserAsync_NonExistingUser_ReturnErrorResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse =
            await _usersService.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveGuestUserWithoutApplicationUserAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _usersRepositoryMock.Setup(repository => repository.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to delete a user";

        // Act
        UsersServiceResponse resultResponse =
            await _usersService.RemoveGuestUserWithoutApplicationUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Equal(expectedErrorMessage, resultResponse.Message);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task GetTeacherUsersAsync_NotEmptyTeacherUsersList_ReturnSuccessResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        IReadOnlyList<TeacherUser> expectedUsers = new List<TeacherUser>
        {
            It.IsAny<TeacherUser>(),
            It.IsAny<TeacherUser>()
        };

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            TeacherUsers = expectedUsers
        };

        _usersRepositoryMock.Setup(repository => repository.GetTeacherUsersAsync())
            .ReturnsAsync(expectedResponse);

        int expectedUsersCount = expectedUsers.Count;

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetTeacherUsersAsync(queryParameters);
        int expectedResultCount = resultResponse.TeacherUsers!.Count;

        // Assert
        Assert.Equal(expectedUsersCount, expectedResultCount);
        Assert.True(resultResponse.IsSuccess);
        Assert.NotEmpty(resultResponse.TeacherUsers);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.NotNull(resultResponse.TeacherUsers);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetTeacherUserDto>>(resultResponse.TeacherUsers);
    }

    [Fact]
    public async Task GetTeacherUsersAsync_EmptyTeacherUsersListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.GetTeacherUsersAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetTeacherUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.NotNull(resultResponse.TeacherUsers);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetTeacherUserDto>>(resultResponse.TeacherUsers);
    }

    [Fact]
    public async Task GetTeacherUsersAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        _usersRepositoryMock.Setup(repository => repository.GetGuestUsersAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to get teacher users";

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetTeacherUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.NotNull(resultResponse.TeacherUsers);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetTeacherUserDto>>(resultResponse.TeacherUsers);
        Assert.Contains(expectedErrorMessage, resultResponse.Message);
    }

    [Fact]
    public async Task RemoveTeacherUserAsync_ExistUser_ReturnSuccessResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveTeacherUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveTeacherUserAsync(It.IsAny<int>());

        // Assert
        Assert.True(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveTeacherUserAsync_NonExistingUser_ReturnErrorResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveTeacherUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveTeacherUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveTeacherUserAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _usersRepositoryMock.Setup(repository => repository.RemoveTeacherUserAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to delete a user";

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveTeacherUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Equal(expectedErrorMessage, resultResponse.Message);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task GetStudentUsersAsync_NotEmptyStudentUsersList_ReturnSuccessResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        IReadOnlyList<StudentUser> expectedUsers = new List<StudentUser>
        {
            It.IsAny<StudentUser>(),
            It.IsAny<StudentUser>()
        };

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            StudentUsers = expectedUsers
        };

        _usersRepositoryMock.Setup(repository => repository.GetStudentUsersAsync())
            .ReturnsAsync(expectedResponse);

        int expectedUsersCount = expectedUsers.Count;

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetStudentUsersAsync(queryParameters);
        int expectedResultCount = resultResponse.StudentUsers!.Count;

        // Assert
        Assert.Equal(expectedUsersCount, expectedResultCount);
        Assert.True(resultResponse.IsSuccess);
        Assert.NotEmpty(resultResponse.StudentUsers);
        Assert.NotNull(resultResponse.StudentUsers);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetStudentUserDto>>(resultResponse.StudentUsers);
    }

    [Fact]
    public async Task GetStudentUsersAsync_EmptyStudentUsersListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        UsersRepositoryResponse expectedResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.GetStudentUsersAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetStudentUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.NotNull(resultResponse.StudentUsers);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetStudentUserDto>>(resultResponse.StudentUsers);
    }

    [Fact]
    public async Task GetStudentUsersAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();

        _usersRepositoryMock.Setup(repository => repository.GetGuestUsersAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to get student users";

        // Act
        UsersServiceResponse resultResponse = await _usersService.GetStudentUsersAsync(queryParameters);

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.NotNull(resultResponse.StudentUsers);
        Assert.Empty(resultResponse.TeacherUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Null(resultResponse.GuestUser);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.IsAssignableFrom<IReadOnlyList<GetStudentUserDto>>(resultResponse.StudentUsers);
        Assert.Contains(expectedErrorMessage, resultResponse.Message);
    }

    [Fact]
    public async Task RemoveStudentUserAsync_ExistUser_ReturnSuccessResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveStudentUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveStudentUserAsync(It.IsAny<int>());

        // Assert
        Assert.True(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveStudentUserAsync_NonExistingUser_ReturnErrorResponse()
    {
        // Arrange
        UsersRepositoryResponse expectedRepositoryResponse = new UsersRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _usersRepositoryMock.Setup(repository => repository.RemoveStudentUserAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedRepositoryResponse);

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveStudentUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }

    [Fact]
    public async Task RemoveStudentUserAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _usersRepositoryMock.Setup(repository => repository.RemoveStudentUserAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to delete a user";

        // Act
        UsersServiceResponse resultResponse = await _usersService.RemoveStudentUserAsync(It.IsAny<int>());

        // Assert
        Assert.False(resultResponse.IsSuccess);
        Assert.IsAssignableFrom<UsersServiceResponse>(resultResponse);
        Assert.Equal(expectedErrorMessage, resultResponse.Message);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Empty(resultResponse.GuestUsers!);
        Assert.Empty(resultResponse.StudentUsers!);
        Assert.Null(resultResponse.GuestUser!);
    }
}