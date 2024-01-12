using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Learning;

public class DisciplineGroupService : BaseService<IDisciplineGroupRepository>, IDisciplineGroupService
{
    private readonly ILogger<DisciplineGroupService> _logger;

    public DisciplineGroupService(IDisciplineGroupRepository repository, IMapper mapper, ILogger<DisciplineGroupService> logger) : base(repository, mapper)
    {
        _logger = logger;
    }
    public async Task<DisciplineGroupServiceResponse> RemoveDisciplineGroupAsync(int disciplineId, int groupId)
    {
        try
        {
            DisciplineGroupRepositoryResponse removeResponse = 
                await _repository.RemoveDisciplineGroupAsync(disciplineId, groupId);

            if (!removeResponse.IsSuccess)
            {
                return new DisciplineGroupServiceResponse
                {
                    Message = removeResponse.Message,
                    IsSuccess = false
                };
            }
            
            return new DisciplineGroupServiceResponse
            {
                Message = "",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new DisciplineGroupServiceResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineGroupServiceResponse> CreateDisciplineGroupAsync(NewDisciplineGroupDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new DisciplineGroupServiceResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }

            DisciplineGroup dtoResult = _mapper.Map<DisciplineGroup>(dto);

            DisciplineGroupRepositoryResponse createResponse = 
                await _repository.CreateDisciplineGroupAsync(dtoResult);

            if (!createResponse.IsSuccess)
            {
                return new DisciplineGroupServiceResponse
                {
                    Message = "",
                    IsSuccess = false
                };
            }
            
            return new DisciplineGroupServiceResponse
            {
                Message = "",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new DisciplineGroupServiceResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }
}