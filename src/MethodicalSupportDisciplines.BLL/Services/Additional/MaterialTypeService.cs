using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class MaterialTypeService : BaseService<IMaterialTypeRepository>, IMaterialTypeService
{
    private readonly ILogger<MaterialTypeService> _logger;

    public MaterialTypeService(IMaterialTypeRepository repository, IMapper mapper,
        ILogger<MaterialTypeService> logger)
        : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<MaterialTypeServiceResponse> GetMaterialTypesASync()
    {
        try
        {
            MaterialTypeRepositoryResponse getResult = await _repository.GetMaterialTypesAsync();

            if (!getResult.IsSuccess)
            {
                return new MaterialTypeServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<MaterialTypeDto> dtoResult = 
                _mapper.Map<IReadOnlyList<MaterialTypeDto>>(getResult.MaterialTypes);

            return new MaterialTypeServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                MaterialTypes = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve material types from the database.");

            return new MaterialTypeServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve material types from the database",
                IsSuccess = false
            };
        }
    }
}