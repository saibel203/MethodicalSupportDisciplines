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

public class FormatLearningServiceUnitTests
{
    private readonly IFormatLearningService _formatLearningService;
    private readonly Mock<IFormatLearningRepository> _formatLearningRepositoryMock;

    public FormatLearningServiceUnitTests()
    {
        _formatLearningRepositoryMock = new Mock<IFormatLearningRepository>();
        Mock<ILogger<FormatLearningService>> loggerMock = new Mock<ILogger<FormatLearningService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _formatLearningService = new FormatLearningService(_formatLearningRepositoryMock.Object,
            loggerMock.Object, mapperMock);
    }

    [Fact]
    public async Task GetFormatLearningsAsync_NotEmptyFormatLearningList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<FormatLearning> expectedFormatLearningList = new List<FormatLearning>
        {
            It.IsAny<FormatLearning>(),
            It.IsAny<FormatLearning>()
        };

        FormatLearningRepositoryResponse expectedResponse = new FormatLearningRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            FormatLearnings = expectedFormatLearningList
        };

        int expectedCount = expectedFormatLearningList.Count;

        _formatLearningRepositoryMock.Setup(repository => repository.GetAllFormatLearningsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        FormatLearningServiceResponse getAllFormatLearningResult =
            await _formatLearningService.GetFormatLearningsAsync();
        int expectedResultCount = getAllFormatLearningResult.FormatLearnings.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllFormatLearningResult.IsSuccess);
        Assert.NotEmpty(getAllFormatLearningResult.FormatLearnings);
        Assert.NotNull(getAllFormatLearningResult.FormatLearnings);
        Assert.IsAssignableFrom<FormatLearningServiceResponse>(getAllFormatLearningResult);
        Assert.IsAssignableFrom<IReadOnlyList<FormatLearningDto>>(getAllFormatLearningResult.FormatLearnings);
    }

    [Fact]
    public async Task GetFormatLearningsAsync_EmptyFormatLearningListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        FormatLearningRepositoryResponse expectedResponse = new FormatLearningRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _formatLearningRepositoryMock.Setup(repository => repository.GetAllFormatLearningsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        FormatLearningServiceResponse getAllFormatLearningsResult =
            await _formatLearningService.GetFormatLearningsAsync();

        // Assert
        Assert.False(getAllFormatLearningsResult.IsSuccess);
        Assert.Empty(getAllFormatLearningsResult.FormatLearnings);
        Assert.NotNull(getAllFormatLearningsResult.FormatLearnings);
        Assert.IsAssignableFrom<FormatLearningServiceResponse>(getAllFormatLearningsResult);
        Assert.IsAssignableFrom<IReadOnlyList<FormatLearningDto>>(getAllFormatLearningsResult.FormatLearnings);
    }
    
    [Fact]
    public async Task GetFormatLearningsAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _formatLearningRepositoryMock.Setup(repository => repository.GetAllFormatLearningsAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of format learnings from the database";

        // Act
        FormatLearningServiceResponse getAllFormatLearningsResult = await _formatLearningService.GetFormatLearningsAsync();

        // Assert
        Assert.False(getAllFormatLearningsResult.IsSuccess);
        Assert.Empty(getAllFormatLearningsResult.FormatLearnings);
        Assert.NotNull(getAllFormatLearningsResult.FormatLearnings);
        Assert.IsAssignableFrom<FormatLearningServiceResponse>(getAllFormatLearningsResult);
        Assert.IsAssignableFrom<IReadOnlyList<FormatLearningDto>>(getAllFormatLearningsResult.FormatLearnings);
        Assert.Contains(expectedErrorMessage, getAllFormatLearningsResult.Message);
    }
}