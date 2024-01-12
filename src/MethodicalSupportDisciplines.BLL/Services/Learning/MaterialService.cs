using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Learning;

public class MaterialService : BaseService<IMaterialRepository>, IMaterialService
{
    private readonly ILogger<MaterialService> _logger;
    private readonly IStringLocalizer<MaterialService> _stringLocalization;

    public MaterialService(IMaterialRepository repository, IMapper mapper, ILogger<MaterialService> logger,
        IStringLocalizer<MaterialService> stringLocalization) : base(
        repository, mapper)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<MaterialServiceResponse> CreateMaterialAsync(NewMaterialDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new MaterialServiceResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }

            Material createdDto = _mapper.Map<Material>(dto);
            MaterialRepositoryResponse createdMaterialResult =
                await _repository.CreateMaterialAsync(createdDto);

            if (!createdMaterialResult.IsSuccess)
            {
                return new MaterialServiceResponse
                {
                    Message = createdMaterialResult.Message,
                    IsSuccess = false
                };
            }

            return new MaterialServiceResponse
            {
                Message = createdMaterialResult.Message,
                IsSuccess = true,
                CreatedMaterialId = createdMaterialResult.CreatedMaterialId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create a new material.");

            return new MaterialServiceResponse
            {
                Message = "An unknown error occurred while trying to create a new material",
                IsSuccess = false
            };
        }
    }

    public async Task<MaterialServiceResponse> RemoveMaterialAsync(int materialId)
    {
        try
        {
            MaterialRepositoryResponse removeResult = await _repository.RemoveMaterialAsync(materialId);

            if (!removeResult.IsSuccess)
            {
                return new MaterialServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            NewMaterialDto returnDto = _mapper.Map<NewMaterialDto>(removeResult.Material);

            return new MaterialServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true,
                Material = returnDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete material from the database");

            return new MaterialServiceResponse
            {
                Message = "An unknown error occurred while trying to delete material from the database",
                IsSuccess = false
            };
        }
    }
}