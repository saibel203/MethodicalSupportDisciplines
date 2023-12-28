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

public class LearningStatusServiceUnitTests
{
    private readonly ILearningStatusService _learningStatusService;
    private readonly Mock<ILearningStatusRepository> _learningStatusRepositoryMock;

    public LearningStatusServiceUnitTests()
    {
        _learningStatusRepositoryMock = new Mock<ILearningStatusRepository>();
        Mock<ILogger<LearningStatusService>> loggerMock = new Mock<ILogger<LearningStatusService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _learningStatusService = new LearningStatusService(_learningStatusRepositoryMock.Object,
            mapperMock, loggerMock.Object);
    }

    [Fact]
    public async Task GetAllLearningStatusesAsync_NotEmptyLearningStatusList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<LearningStatus> expectedGroupList = new List<LearningStatus>
        {
            It.IsAny<LearningStatus>(),
            It.IsAny<LearningStatus>()
        };

        LearningStatusRepositoryResponse expectedResponse = new LearningStatusRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            LearningStatuses = expectedGroupList
        };

        int expectedCount = expectedGroupList.Count;

        _learningStatusRepositoryMock.Setup(repository => repository.GetAllLearningStatusesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        LearningStatusServiceResponse getAllLearningStatusesResult =
            await _learningStatusService.GetAllLearningStatusesAsync();
        int expectedResultCount = getAllLearningStatusesResult.LearningStatuses.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllLearningStatusesResult.IsSuccess);
        Assert.NotEmpty(getAllLearningStatusesResult.LearningStatuses);
        Assert.NotNull(getAllLearningStatusesResult.LearningStatuses);
        Assert.IsAssignableFrom<LearningStatusServiceResponse>(getAllLearningStatusesResult);
        Assert.IsAssignableFrom<IReadOnlyList<LearningStatusDto>>(getAllLearningStatusesResult.LearningStatuses);
    }

    [Fact]
    public async Task GetAllLearningStatusesAsync_EmptyLearningStatusesFromRepository_ReturnErrorResponse()
    {
        // Arrange
        LearningStatusRepositoryResponse expectedResponse = new LearningStatusRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _learningStatusRepositoryMock.Setup(repository => repository.GetAllLearningStatusesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        LearningStatusServiceResponse getAllLearningStatusesResult =
            await _learningStatusService.GetAllLearningStatusesAsync();

        // Assert
        Assert.False(getAllLearningStatusesResult.IsSuccess);
        Assert.Empty(getAllLearningStatusesResult.LearningStatuses);
        Assert.NotNull(getAllLearningStatusesResult.LearningStatuses);
        Assert.IsAssignableFrom<LearningStatusServiceResponse>(getAllLearningStatusesResult);
        Assert.IsAssignableFrom<IReadOnlyList<LearningStatusDto>>(getAllLearningStatusesResult.LearningStatuses);
    }
    
    [Fact]
    public async Task GetAllLearningStatusesAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _learningStatusRepositoryMock.Setup(repository => repository.GetAllLearningStatusesAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of learning statuses from the database";

        // Act
        LearningStatusServiceResponse getAllLearningStatusesResult = await _learningStatusService.GetAllLearningStatusesAsync();

        // Assert
        Assert.False(getAllLearningStatusesResult.IsSuccess);
        Assert.Empty(getAllLearningStatusesResult.LearningStatuses);
        Assert.NotNull(getAllLearningStatusesResult.LearningStatuses);
        Assert.IsAssignableFrom<LearningStatusServiceResponse>(getAllLearningStatusesResult);
        Assert.IsAssignableFrom<IReadOnlyList<LearningStatusDto>>(getAllLearningStatusesResult.LearningStatuses);
        Assert.Contains(expectedErrorMessage, getAllLearningStatusesResult.Message);
    }
}