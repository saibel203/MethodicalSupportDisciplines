using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.BLL.Services.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using MethodicalSupportDisciplines.UnitTests.Helpers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MethodicalSupportDisciplines.BLL.UnitTests;

public class DisciplineServiceUnitTests
{
    private readonly IDisciplineService _disciplineService;
    private readonly Mock<IDisciplineRepository> _disciplineRepositoryMock;

    public DisciplineServiceUnitTests()
    {
        Mock<ILogger<DisciplineService>> loggerMock = 
            new Mock<ILogger<DisciplineService>>();
        Mock<IStringLocalizer<DisciplineService>> stringLocalizationMock =
            new Mock<IStringLocalizer<DisciplineService>>();
        IMapper mapperMock = MapperMock.GetMapper();

        _disciplineRepositoryMock = new Mock<IDisciplineRepository>();

        _disciplineService = new DisciplineService(
            _disciplineRepositoryMock.Object, mapperMock,
            loggerMock.Object,
            stringLocalizationMock.Object);
    }

    [Fact]
    public async Task GetAllDisciplinesAsync_NotEmptyDisciplinesList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Discipline> responseList = new List<Discipline>
        {
            It.IsAny<Discipline>(),
            It.IsAny<Discipline>()
        };
        
        QueryParameters queryParameters = new QueryParameters();
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Disciplines = responseList
        };

        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesAsync(It.IsAny<string>()))
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result =
            await _disciplineService.GetAllDisciplinesAsync(queryParameters, string.Empty);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.NotEmpty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }
    
    [Fact]
    public async Task GetAllDisciplinesAsync_ErrorFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesAsync(It.IsAny<string>()))
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result =
            await _disciplineService.GetAllDisciplinesAsync(queryParameters, string.Empty);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }
    
    [Fact]
    public async Task GetAllDisciplinesAsync_UnknownServiceError_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();
        
        const string expectedErrorMessage = 
            "An unknown error occurred while trying to retrieve the list of disciplines";
        
        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated exception"));
        
        // Act
        DisciplineServiceResponse result =
            await _disciplineService.GetAllDisciplinesAsync(queryParameters, string.Empty);

        // Assert
        Assert.Equal(expectedErrorMessage, result.Message);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }

    [Fact]
    public async Task GetAllDisciplinesForAdminAsync_NotEmptyDisciplinesList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Discipline> responseList = new List<Discipline>
        {
            It.IsAny<Discipline>(),
            It.IsAny<Discipline>()
        };
        
        QueryParameters queryParameters = new QueryParameters();
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Disciplines = responseList
        };

        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesForAdminAsync())
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result = 
            await _disciplineService.GetAllDisciplinesForAdminAsync(queryParameters);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.NotEmpty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }
    
    [Fact]
    public async Task GetAllDisciplinesForAdminAsync_ErrorFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };

        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesForAdminAsync())
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result =
            await _disciplineService.GetAllDisciplinesForAdminAsync(queryParameters);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }
    
    [Fact]
    public async Task GetAllDisciplinesForAdminAsync_UnknownServiceError_ReturnErrorResponse()
    {
        // Arrange
        QueryParameters queryParameters = new QueryParameters();
        
        const string expectedErrorMessage = 
            "An unknown error occurred while trying to retrieve the list of disciplines";
        
        _disciplineRepositoryMock.Setup(repository => repository.GetAllDisciplinesForAdminAsync())
            .ThrowsAsync(new Exception("Simulated exception"));
        
        // Act
        DisciplineServiceResponse result =
            await _disciplineService.GetAllDisciplinesForAdminAsync(queryParameters);

        // Assert
        Assert.Equal(expectedErrorMessage, result.Message);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineActionDto>>(result.Disciplines);
    }
    
    [Fact]
    public async Task GetDisciplineByIdAsync_ExistsDiscipline_ReturnSuccessResponse()
    {
        // Arrange
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Discipline = new Discipline()
        };
        
        _disciplineRepositoryMock.Setup(repository => repository.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result = 
            await _disciplineService.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>());
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.NotNull(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
        Assert.IsAssignableFrom<DisciplineActionDto>(result.Discipline);
    }
    
    [Fact]
    public async Task GetDisciplineByIdAsync_ErrorFromRepository_ReturnSuccessResponse()
    {
        // Arrange
        DisciplineRepositoryResponse methodResponse = new DisciplineRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };
        
        _disciplineRepositoryMock.Setup(repository => repository.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(methodResponse);
        
        // Act
        DisciplineServiceResponse result = 
            await _disciplineService.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>());
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
    }
    
    [Fact]
    public async Task GetDisciplineByIdAsync_UnknownServiceError_ReturnErrorResponse()
    {
        // Arrange
        _disciplineRepositoryMock.Setup(repository => repository.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Simulated exception"));
        
        const string expectedErrorMessage = 
            "An unknown error occurred while trying to obtain discipline";
        
        // Act
        DisciplineServiceResponse result = 
            await _disciplineService.GetDisciplineByIdAsync(It.IsAny<int>(), It.IsAny<string>());
        
        // Assert
        Assert.Equal(expectedErrorMessage, result.Message);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Disciplines);
        Assert.Empty(result.Disciplines);
        Assert.Null(result.Discipline);
        Assert.IsAssignableFrom<DisciplineServiceResponse>(result);
    }
}