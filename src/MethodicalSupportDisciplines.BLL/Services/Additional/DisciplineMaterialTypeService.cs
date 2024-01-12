using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class DisciplineMaterialTypeService : BaseService<IDisciplineMaterialTypeRepository>,
    IDisciplineMaterialTypeService
{
    private readonly ILogger<DisciplineMaterialTypeService> _logger;


    public DisciplineMaterialTypeService(IDisciplineMaterialTypeRepository repository, IMapper mapper,
        ILogger<DisciplineMaterialTypeService> logger) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<DisciplineMaterialTypeServiceResponse> GetDisciplineMaterialTypesAsync()
    {
        try
        {
            DisciplineMaterialTypeRepositoryResponse getTypesResponse =
                await _repository.GetDisciplineMaterialTypesAsync();

            if (!getTypesResponse.IsSuccess)
            {
                return new DisciplineMaterialTypeServiceResponse
                {
                    Message = getTypesResponse.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<DisciplineMaterialTypeDto> dtoResult =
                _mapper.Map<IReadOnlyList<DisciplineMaterialTypeDto>>(getTypesResponse.DisciplineMaterialTypes);

            return new DisciplineMaterialTypeServiceResponse
            {
                Message = getTypesResponse.Message,
                IsSuccess = true,
                DisciplineMaterials = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown error occurred while trying to get material types.");

            return new DisciplineMaterialTypeServiceResponse
            {
                Message = "Unknown error occurred while trying to get material types",
                IsSuccess = false
            };
        }
    }
}