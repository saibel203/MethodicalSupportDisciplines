using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.BLL.Services.Additional;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using MethodicalSupportDisciplines.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MethodicalSupportDisciplines.BLL.UnitTests;

public class FacultyServiceUnitTests
{
    private readonly IFacultyService _facultyService;
    private readonly Mock<IFacultyRepository> _facultyRepositoryMock;

    public FacultyServiceUnitTests()
    {
        _facultyRepositoryMock = new Mock<IFacultyRepository>();
        Mock<ILogger<FacultyService>> loggerMock = new Mock<ILogger<FacultyService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _facultyService = new FacultyService(_facultyRepositoryMock.Object,
            loggerMock.Object, mapperMock);
    }

    [Fact]
    public async Task GetAllFacultiesAsync_NotEmptyFacultyList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Faculty> expectedFacultyList = new List<Faculty>
        {
            It.IsAny<Faculty>(),
            It.IsAny<Faculty>()
        };

        FacultyRepositoryResponse expectedResponse = new FacultyRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Faculties = expectedFacultyList
        };

        int expectedCount = expectedFacultyList.Count;

        _facultyRepositoryMock.Setup(repository => repository.GetAllFacultiesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        FacultyServiceResponse getAllFacultiesResult = await _facultyService.GetAllFacultiesAsync();
        int expectedResultCount = getAllFacultiesResult.Faculties.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllFacultiesResult.IsSuccess);
        Assert.NotEmpty(getAllFacultiesResult.Faculties);
        Assert.NotNull(getAllFacultiesResult.Faculties);
        Assert.IsAssignableFrom<FacultyServiceResponse>(getAllFacultiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<FacultyDto>>(getAllFacultiesResult.Faculties);
    }

    [Fact]
    public async Task GetAllFacultiesAsync_EmptyFacultyListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        FacultyRepositoryResponse expectedResponse = new FacultyRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _facultyRepositoryMock.Setup(repository => repository.GetAllFacultiesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        FacultyServiceResponse getAllFacultiesResult = await _facultyService.GetAllFacultiesAsync();

        // Assert
        Assert.False(getAllFacultiesResult.IsSuccess);
        Assert.Empty(getAllFacultiesResult.Faculties);
        Assert.NotNull(getAllFacultiesResult.Faculties);
        Assert.IsAssignableFrom<FacultyServiceResponse>(getAllFacultiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<FacultyDto>>(getAllFacultiesResult.Faculties);
    }
    
    [Fact]
    public async Task GetAllFacultiesAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _facultyRepositoryMock.Setup(repository => repository.GetAllFacultiesAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of faculties from the database";

        // Act
        FacultyServiceResponse getAllFacultiesResult = await _facultyService.GetAllFacultiesAsync();

        // Assert
        Assert.False(getAllFacultiesResult.IsSuccess);
        Assert.Empty(getAllFacultiesResult.Faculties);
        Assert.NotNull(getAllFacultiesResult.Faculties);
        Assert.IsAssignableFrom<FacultyServiceResponse>(getAllFacultiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<FacultyDto>>(getAllFacultiesResult.Faculties);
        Assert.Contains(expectedErrorMessage, getAllFacultiesResult.Message);
    }
}