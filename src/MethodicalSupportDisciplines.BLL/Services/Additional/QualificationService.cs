using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class QualificationService : BaseService<IQualificationRepository>, IQualificationService
{
    private readonly ILogger<QualificationService> _logger;

    public QualificationService(IQualificationRepository repository, ILogger<QualificationService> logger,
        IMapper mapper) : base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<QualificationServiceResponse> GetQualificationsAsync()
    {
        try
        {
            QualificationRepositoryResponse getResult = await _repository.GetAllQualificationsAsync();

            if (!getResult.IsSuccess)
            {
                return new QualificationServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<QualificationDto> dtoResult =
                _mapper.Map<IReadOnlyList<QualificationDto>>(getResult.Qualifications);

            return new QualificationServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                Qualifications = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of qualifications from the database.");

            return new QualificationServiceResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of qualifications from the database",
                IsSuccess = false
            };
        }
    }
}