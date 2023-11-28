using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces;
using MethodicalSupportDisciplines.BLL.Services;
using MethodicalSupportDisciplines.Core.IOptions;
using MethodicalSupportDisciplines.Core.Models.Identity;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.Responses.Services;
using MethodicalSupportDisciplines.UnitTests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace MethodicalSupportDisciplines.BLL.UnitTests;

public class AuthServiceUnitTests
{
    /*private readonly IAuthService _authService;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private readonly Mock<IOptions<WebPathsOptions>> _webPathsOptionsMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly Mock<IMailService> _mailServiceMock;
    private readonly Mock<DataDbContext> _dbContextMock;

    public AuthServiceUnitTests()
    {
        List<ApplicationUser> userList = new List<ApplicationUser>(); 
        
        _userManagerMock = UserManagerMock.MockUserManager(userList);
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>();
        _roleManagerMock = new Mock<RoleManager<ApplicationRole>>();
        _webPathsOptionsMock = new Mock<IOptions<WebPathsOptions>>();
        _loggerMock = new Mock<ILogger<AuthService>>();
        _mailServiceMock = new Mock<IMailService>();
        _dbContextMock = new Mock<DataDbContext>();

        IMapper mapper = MapperMock.GetMapper();
        
        _authService = new AuthService(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _roleManagerMock.Object,
            _webPathsOptionsMock.Object,
            _loggerMock.Object, mapper,
            _mailServiceMock.Object,
            _dbContextMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_GetNullValue_FalseResponse()
    {
        // Arrange
        UserRegisterDto? dto = null;
        const string expectedErrorMessage = "The method received an incorrect value, possibly null";
        
        // Act
        UserAuthResponse registerAsyncResult = await _authService
            .RegisterAsync(dto);
        bool isRegisterSuccess = registerAsyncResult.IsSuccess;
        string registerErrorMessage = registerAsyncResult.Message;

        // Assert
        Assert.False(isRegisterSuccess);
        Assert.Equal(expectedErrorMessage, registerErrorMessage);
    }*/
}