using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class LearningStatusService : BaseService<ILearningStatusRepository>, ILearningStatusService
{
    private readonly ILogger<LearningStatusService> _logger;

    public LearningStatusService(ILearningStatusRepository repository, IMapper mapper,
        ILogger<LearningStatusService> logger) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<LearningStatusServiceResponse> GetAllLearningStatusesAsync()
    {
        try
        {
            LearningStatusRepositoryResponse getResult = await _repository.GetAllLearningStatusesASync();

            if (!getResult.IsSuccess)
            {
                return new LearningStatusServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<LearningStatusDto> dtoResult =
                _mapper.Map<IReadOnlyList<LearningStatusDto>>(getResult.LearningStatuses);

            return new LearningStatusServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                LearningStatuses = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of learning statuses from the database.");

            return new LearningStatusServiceResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of learning statuses from the database",
                IsSuccess = false
            };
        }
    }
}