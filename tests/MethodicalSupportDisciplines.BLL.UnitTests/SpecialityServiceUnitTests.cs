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

public class SpecialityServiceUnitTests
{
    private readonly ISpecialityService _specialityService;
    private readonly Mock<ISpecialityRepository> _specialityRepositoryMock;

    public SpecialityServiceUnitTests()
    {
        _specialityRepositoryMock = new Mock<ISpecialityRepository>();
        Mock<ILogger<SpecialityService>> loggerMock = new Mock<ILogger<SpecialityService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _specialityService = new SpecialityService(_specialityRepositoryMock.Object,
            mapperMock, loggerMock.Object);
    }

    [Fact]
    public async Task GetAllSpecialitiesAsync_NotEmptySpecialitiesList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Specialty> expectedGroupList = new List<Specialty>
        {
            It.IsAny<Specialty>(),
            It.IsAny<Specialty>()
        };

        SpecialityRepositoryResponse expectedResponse = new SpecialityRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Specialties = expectedGroupList
        };

        int expectedCount = expectedGroupList.Count;

        _specialityRepositoryMock.Setup(repository => repository.GetAllSpecialitiesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        SpecialityServiceResponse getAllSpecialitiesResult =
            await _specialityService.GetAllSpecialitiesAsync();
        int expectedResultCount = getAllSpecialitiesResult.Specialties.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllSpecialitiesResult.IsSuccess);
        Assert.NotEmpty(getAllSpecialitiesResult.Specialties);
        Assert.NotNull(getAllSpecialitiesResult.Specialties);
        Assert.IsAssignableFrom<SpecialityServiceResponse>(getAllSpecialitiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<SpecialityDto>>(getAllSpecialitiesResult.Specialties);
    }

    [Fact]
    public async Task GetAllSpecialitiesAsync_EmptySpecialitiesListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        SpecialityRepositoryResponse expectedResponse = new SpecialityRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _specialityRepositoryMock.Setup(repository => repository.GetAllSpecialitiesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        SpecialityServiceResponse getAllSpecialitiesResult =
            await _specialityService.GetAllSpecialitiesAsync();

        // Assert
        Assert.False(getAllSpecialitiesResult.IsSuccess);
        Assert.Empty(getAllSpecialitiesResult.Specialties);
        Assert.NotNull(getAllSpecialitiesResult.Specialties);
        Assert.IsAssignableFrom<SpecialityServiceResponse>(getAllSpecialitiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<SpecialityDto>>(getAllSpecialitiesResult.Specialties);
    }
    
    [Fact]
    public async Task GetAllSpecialitiesAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _specialityRepositoryMock.Setup(repository => repository.GetAllSpecialitiesAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of specialities from the database";

        // Act
        SpecialityServiceResponse getAllSpecialitiesResult = await _specialityService.GetAllSpecialitiesAsync();

        // Assert
        Assert.False(getAllSpecialitiesResult.IsSuccess);
        Assert.Empty(getAllSpecialitiesResult.Specialties);
        Assert.NotNull(getAllSpecialitiesResult.Specialties);
        Assert.IsAssignableFrom<SpecialityServiceResponse>(getAllSpecialitiesResult);
        Assert.IsAssignableFrom<IReadOnlyList<SpecialityDto>>(getAllSpecialitiesResult.Specialties);
        Assert.Contains(expectedErrorMessage, getAllSpecialitiesResult.Message);
    }
}