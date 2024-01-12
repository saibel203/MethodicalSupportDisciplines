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

public class DisciplineMaterialService : BaseService<IDisciplineMaterialRepository>, IDisciplineMaterialService
{
    private readonly ILogger<DisciplineMaterialService> _logger;
    private readonly IStringLocalizer<DisciplineMaterialService> _stringLocalization;

    public DisciplineMaterialService(IDisciplineMaterialRepository repository, IMapper mapper,
        ILogger<DisciplineMaterialService> logger,
        IStringLocalizer<DisciplineMaterialService> stringLocalization) : base(repository, mapper)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<DisciplineMaterialServiceResponse> RemoveDisciplineMaterialAsync(int disciplineMaterialId)
    {
        try
        {
            DisciplineMaterialRepositoryResponse removeResult =
                await _repository.RemoveDisciplineMaterialAsync(disciplineMaterialId);

            if (!removeResult.IsSuccess)
            {
                return new DisciplineMaterialServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            DisciplineMaterialActionDto returnDto =
                _mapper.Map<DisciplineMaterialActionDto>(removeResult.DisciplineMaterial);

            return new DisciplineMaterialServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true,
                DisciplineMaterial = returnDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a class.");

            return new DisciplineMaterialServiceResponse
            {
                Message = "An unknown error occurred while trying to delete a class",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineMaterialServiceResponse> CreateDisciplineMaterialAsync(NewDisciplineMaterialDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new DisciplineMaterialServiceResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }

            DisciplineMaterial dtoResult = _mapper.Map<DisciplineMaterial>(dto);

            DisciplineMaterialRepositoryResponse createResponse =
                await _repository.CreateDisciplineMaterialAsync(dtoResult);

            if (!createResponse.IsSuccess)
            {
                return new DisciplineMaterialServiceResponse
                {
                    Message = createResponse.Message,
                    IsSuccess = false
                };
            }

            return new DisciplineMaterialServiceResponse
            {
                Message = createResponse.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create new material for the course.");

            return new DisciplineMaterialServiceResponse
            {
                Message = "An unknown error occurred while trying to create new material for the course",
                IsSuccess = false
            };
        }
    }
}