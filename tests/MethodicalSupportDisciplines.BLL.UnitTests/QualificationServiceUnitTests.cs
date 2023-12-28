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

public class QualificationServiceUnitTests
{
    private readonly IQualificationService _qualificationService;
    private readonly Mock<IQualificationRepository> _qualificationRepositoryMock;

    public QualificationServiceUnitTests()
    {
        _qualificationRepositoryMock = new Mock<IQualificationRepository>();
        Mock<ILogger<QualificationService>> loggerMock = new Mock<ILogger<QualificationService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _qualificationService = new QualificationService(_qualificationRepositoryMock.Object,
            loggerMock.Object, mapperMock);
    }

    [Fact]
    public async Task GetQualificationsAsync_NotEmptyQualificationList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Qualification> expectedGroupList = new List<Qualification>
        {
            It.IsAny<Qualification>(),
            It.IsAny<Qualification>()
        };

        QualificationRepositoryResponse expectedResponse = new QualificationRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Qualifications = expectedGroupList
        };

        int expectedCount = expectedGroupList.Count;

        _qualificationRepositoryMock.Setup(repository => repository.GetAllQualificationsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        QualificationServiceResponse getAllQualificationsResult =
            await _qualificationService.GetQualificationsAsync();
        int expectedResultCount = getAllQualificationsResult.Qualifications.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllQualificationsResult.IsSuccess);
        Assert.NotEmpty(getAllQualificationsResult.Qualifications);
        Assert.NotNull(getAllQualificationsResult.Qualifications);
        Assert.IsAssignableFrom<QualificationServiceResponse>(getAllQualificationsResult);
        Assert.IsAssignableFrom<IReadOnlyList<QualificationDto>>(getAllQualificationsResult.Qualifications);
    }

    [Fact]
    public async Task GetQualificationsAsync_EmptyQualificationsListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        QualificationRepositoryResponse expectedResponse = new QualificationRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _qualificationRepositoryMock.Setup(repository => repository.GetAllQualificationsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        QualificationServiceResponse getAllQualificationsResult =
            await _qualificationService.GetQualificationsAsync();

        // Assert
        Assert.False(getAllQualificationsResult.IsSuccess);
        Assert.Empty(getAllQualificationsResult.Qualifications);
        Assert.NotNull(getAllQualificationsResult.Qualifications);
        Assert.IsAssignableFrom<QualificationServiceResponse>(getAllQualificationsResult);
        Assert.IsAssignableFrom<IReadOnlyList<QualificationDto>>(getAllQualificationsResult.Qualifications);
    }
    
    [Fact]
    public async Task GetQualificationsAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _qualificationRepositoryMock.Setup(repository => repository.GetAllQualificationsAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of qualifications from the database";

        // Act
        QualificationServiceResponse getAllQualificationsResult = await _qualificationService.GetQualificationsAsync();

        // Assert
        Assert.False(getAllQualificationsResult.IsSuccess);
        Assert.Empty(getAllQualificationsResult.Qualifications);
        Assert.NotNull(getAllQualificationsResult.Qualifications);
        Assert.IsAssignableFrom<QualificationServiceResponse>(getAllQualificationsResult);
        Assert.IsAssignableFrom<IReadOnlyList<QualificationDto>>(getAllQualificationsResult.Qualifications);
        Assert.Contains(expectedErrorMessage, getAllQualificationsResult.Message);
    }
}