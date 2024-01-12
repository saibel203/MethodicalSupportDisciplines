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

public class DisciplineMaterialTypeServiceUnitTests
{
    private readonly IDisciplineMaterialTypeService _disciplineMaterialTypeService;
    private readonly Mock<IDisciplineMaterialTypeRepository> _disciplineMaterialTypeRepositoryMock;

    public DisciplineMaterialTypeServiceUnitTests()
    {
        _disciplineMaterialTypeRepositoryMock = new Mock<IDisciplineMaterialTypeRepository>();

        Mock<ILogger<DisciplineMaterialTypeService>> loggerMock = new Mock<ILogger<DisciplineMaterialTypeService>>();
        IMapper mapperMock = MapperMock.GetMapper();

        _disciplineMaterialTypeService = new DisciplineMaterialTypeService(
            _disciplineMaterialTypeRepositoryMock.Object, mapperMock,
            loggerMock.Object);
    }

    [Fact]
    public async Task GetDisciplineMaterialTypesAsync_NotEmptyMaterialTypesList_ReturnSuccessResponse()
    {
        // Arrange
        IReadOnlyList<DisciplineMaterialType> expectedList = new List<DisciplineMaterialType>
        {
            It.IsAny<DisciplineMaterialType>(),
            It.IsAny<DisciplineMaterialType>()
        };

        DisciplineMaterialTypeRepositoryResponse expectedResponse = new DisciplineMaterialTypeRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = true,
            DisciplineMaterialTypes = expectedList
        };

        int expectedCount = expectedList.Count;

        _disciplineMaterialTypeRepositoryMock.Setup(repository => repository.GetDisciplineMaterialTypesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        DisciplineMaterialTypeServiceResponse getDisciplineMaterialTypesAsyncResult =
            await _disciplineMaterialTypeService.GetDisciplineMaterialTypesAsync();
        int expectedResultCount = getDisciplineMaterialTypesAsyncResult.DisciplineMaterials.Count;

        // Assert
        Assert.Equal(expectedCount, expectedResultCount);
        Assert.True(getDisciplineMaterialTypesAsyncResult.IsSuccess);
        Assert.NotEmpty(getDisciplineMaterialTypesAsyncResult.DisciplineMaterials);
        Assert.NotNull(getDisciplineMaterialTypesAsyncResult.DisciplineMaterials);
        Assert.IsAssignableFrom<DisciplineMaterialTypeServiceResponse>(getDisciplineMaterialTypesAsyncResult);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineMaterialTypeDto>>(getDisciplineMaterialTypesAsyncResult
            .DisciplineMaterials);
    }

    [Fact]
    public async Task
        GetDisciplineMaterialTypesAsync_EmptyDisciplineMaterialTypesListFromRepository_ReturnErrorResponse()
    {
        // Arrange
        DisciplineMaterialTypeRepositoryResponse expectedResponse = new DisciplineMaterialTypeRepositoryResponse
        {
            Message = string.Empty,
            IsSuccess = false
        };
        
        _disciplineMaterialTypeRepositoryMock.Setup(repository => repository.GetDisciplineMaterialTypesAsync())
            .ReturnsAsync(expectedResponse);

        // Act
        DisciplineMaterialTypeServiceResponse getDisciplineMaterialTypesAsyncResult =
            await _disciplineMaterialTypeService.GetDisciplineMaterialTypesAsync();

        // Assert
        Assert.False(getDisciplineMaterialTypesAsyncResult.IsSuccess);
        Assert.Empty(getDisciplineMaterialTypesAsyncResult.DisciplineMaterials);
        Assert.NotNull(getDisciplineMaterialTypesAsyncResult.DisciplineMaterials);
        Assert.IsAssignableFrom<DisciplineMaterialTypeServiceResponse>(getDisciplineMaterialTypesAsyncResult);
        Assert.IsAssignableFrom<IReadOnlyList<DisciplineMaterialTypeDto>>(getDisciplineMaterialTypesAsyncResult
            .DisciplineMaterials);
    }
}