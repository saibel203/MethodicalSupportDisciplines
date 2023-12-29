using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.BLL.Services.Additional;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using MethodicalSupportDisciplines.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MethodicalSupportDisciplines.BLL.UnitTests;

public class GroupServiceUnitTests
{
    private readonly IGroupService _groupService;
    private readonly Mock<IGroupRepository> _groupRepositoryMock;

    public GroupServiceUnitTests()
    {
        _groupRepositoryMock = new Mock<IGroupRepository>();
        Mock<ILogger<GroupService>> loggerMock = new Mock<ILogger<GroupService>>();

        IMapper mapperMock = MapperMock.GetMapper();

        _groupService = new GroupService(_groupRepositoryMock.Object,
            mapperMock, loggerMock.Object);
    }

    [Fact]
    public async Task GetAllGroupsAsync_NotEmptyFormatLearningList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<Group> expectedGroupList = new List<Group>
        {
            It.IsAny<Group>(),
            It.IsAny<Group>()
        };

        GroupRepositoryResponse expectedResponse = new GroupRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            Groups = expectedGroupList
        };

        int expectedCount = expectedGroupList.Count;

        _groupRepositoryMock.Setup(repository => repository.GetAllGroupsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        GroupServiceResponse getAllGroupsResult =
            await _groupService.GetAllGroupsAsync();
        int expectedResultCount = getAllGroupsResult.Groups.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getAllGroupsResult.IsSuccess);
        Assert.NotEmpty(getAllGroupsResult.Groups);
        Assert.NotNull(getAllGroupsResult.Groups);
        Assert.IsAssignableFrom<GroupServiceResponse>(getAllGroupsResult);
        Assert.IsAssignableFrom<IReadOnlyList<GroupDto>>(getAllGroupsResult.Groups);
    }

    [Fact]
    public async Task GetAllGroupsAsync_EmptyGroupsListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        GroupRepositoryResponse expectedResponse = new GroupRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false,
        };

        _groupRepositoryMock.Setup(repository => repository.GetAllGroupsAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        GroupServiceResponse getAllGroupsResult =
            await _groupService.GetAllGroupsAsync();

        // Assert
        Assert.False(getAllGroupsResult.IsSuccess);
        Assert.Empty(getAllGroupsResult.Groups);
        Assert.NotNull(getAllGroupsResult.Groups);
        Assert.IsAssignableFrom<GroupServiceResponse>(getAllGroupsResult);
        Assert.IsAssignableFrom<IReadOnlyList<GroupDto>>(getAllGroupsResult.Groups);
    }

    [Fact]
    public async Task GetAllGroupsAsync_ExceptionThrown_ReturnsErrorResponse()
    {
        // Arrange
        _groupRepositoryMock.Setup(repository => repository.GetAllGroupsAsync())
            .ThrowsAsync(new Exception("Simulated exception"));

        const string expectedErrorMessage =
            "An unknown error occurred while trying to retrieve a list of groups from the database";

        // Act
        GroupServiceResponse getAllGroupsResult = await _groupService.GetAllGroupsAsync();

        // Assert
        Assert.False(getAllGroupsResult.IsSuccess);
        Assert.Empty(getAllGroupsResult.Groups);
        Assert.NotNull(getAllGroupsResult.Groups);
        Assert.IsAssignableFrom<GroupServiceResponse>(getAllGroupsResult);
        Assert.IsAssignableFrom<IReadOnlyList<GroupDto>>(getAllGroupsResult.Groups);
        Assert.Contains(expectedErrorMessage, getAllGroupsResult.Message);
    }
}