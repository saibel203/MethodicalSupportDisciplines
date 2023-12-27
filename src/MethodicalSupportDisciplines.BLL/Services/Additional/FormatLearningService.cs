using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class FormatLearningService : BaseService<IFormatLearningRepository>, IFormatLearningService
{
    private readonly ILogger<FormatLearningService> _logger;

    public FormatLearningService(IFormatLearningRepository repository, ILogger<FormatLearningService> logger,
        IMapper mapper) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<FormatLearningServiceResponse> GetFormatLearningsAsync()
    {
        try
        {
            FormatLearningRepositoryResponse getResult = await _repository.GetAllFormatLearningsAsync();

            if (!getResult.IsSuccess)
            {
                return new FormatLearningServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<FormatLearningDto> dtoResult =
                _mapper.Map<IReadOnlyList<FormatLearningDto>>(getResult.FormatLearnings);

            return new FormatLearningServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                FormatLearnings = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of format learnings from the database.");

            return new FormatLearningServiceResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of format learnings from the database",
                IsSuccess = false
            };
        }
    }
}