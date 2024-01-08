using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Learning;

public class MaterialDisciplineMaterialService : BaseService<IMaterialDisciplineMaterialRepository>,
    IMaterialDisciplineMaterialService
{
    private readonly ILogger<MaterialDisciplineMaterialService> _logger;

    public MaterialDisciplineMaterialService(IMaterialDisciplineMaterialRepository repository, IMapper mapper,
        ILogger<MaterialDisciplineMaterialService> logger) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<MaterialDisciplineMaterialServiceResponse> CreateMaterialDisciplineMaterialAsync(NewMaterialDisciplineMaterialDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new MaterialDisciplineMaterialServiceResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }

            var dtoX = _mapper.Map<MaterialDisciplineMaterial>(dto);

            var response = await _repository.CreateMaterialDisciplineMaterialAsync(dtoX);

            if (!response.IsSuccess)
            {
                return new MaterialDisciplineMaterialServiceResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }
            
            return new MaterialDisciplineMaterialServiceResponse
            {
                Message = "",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new MaterialDisciplineMaterialServiceResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }
}