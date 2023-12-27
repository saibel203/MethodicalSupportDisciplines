using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class SpecialityService : BaseService<ISpecialityRepository>, ISpecialityService
{
    private readonly ILogger<SpecialityService> _logger;
    
    public SpecialityService(ISpecialityRepository repository, IMapper mapper, ILogger<SpecialityService> logger) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<SpecialityServiceResponse> GetAllSpecialitiesAsync()
    {
        try
        {
            SpecialityRepositoryResponse getResult = await _repository.GetAllSpecialitiesAsync();

            if (!getResult.IsSuccess)
            {
                return new SpecialityServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<SpecialityDto> dtoResult = _mapper.Map<IReadOnlyList<SpecialityDto>>(getResult.Specialties);

            return new SpecialityServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                Specialties = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new SpecialityServiceResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }
}