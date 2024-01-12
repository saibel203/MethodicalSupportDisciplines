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

public class MaterialDisciplineMaterialService : BaseService<IMaterialDisciplineMaterialRepository>,
    IMaterialDisciplineMaterialService
{
    private readonly ILogger<MaterialDisciplineMaterialService> _logger;
    private readonly IStringLocalizer<MaterialDisciplineMaterialService> _stringLocalization;

    public MaterialDisciplineMaterialService(IMaterialDisciplineMaterialRepository repository, IMapper mapper,
        ILogger<MaterialDisciplineMaterialService> logger, IStringLocalizer<MaterialDisciplineMaterialService> stringLocalization) : base(repository, mapper)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<MaterialDisciplineMaterialServiceResponse> CreateMaterialDisciplineMaterialAsync(NewMaterialDisciplineMaterialDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new MaterialDisciplineMaterialServiceResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }

            MaterialDisciplineMaterial dtoResult = 
                _mapper.Map<MaterialDisciplineMaterial>(dto);

            MaterialDisciplineMaterialRepositoryResponse createdResponse = 
                await _repository.CreateMaterialDisciplineMaterialAsync(dtoResult);

            if (!createdResponse.IsSuccess)
            {
                return new MaterialDisciplineMaterialServiceResponse
                {
                    Message = createdResponse.Message,
                    IsSuccess = false
                };
            }
            
            return new MaterialDisciplineMaterialServiceResponse
            {
                Message = createdResponse.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to add a new material-disciplineMaterial relationship.");

            return new MaterialDisciplineMaterialServiceResponse
            {
                Message = "An unknown error occurred while trying to add a new material-disciplineMaterial relationship",
                IsSuccess = false
            };
        }
    }
}