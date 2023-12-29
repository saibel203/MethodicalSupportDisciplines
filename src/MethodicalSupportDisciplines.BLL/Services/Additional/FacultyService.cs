using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class FacultyService : BaseService<IFacultyRepository>, IFacultyService
{
    private readonly ILogger<FacultyService> _logger;

    public FacultyService(IFacultyRepository repository, ILogger<FacultyService> logger, IMapper mapper) :
        base(repository, mapper)
    {
        _logger = logger;
    }

    public async Task<FacultyServiceResponse> GetAllFacultiesAsync()
    {
        try
        {
            FacultyRepositoryResponse getResult = await _repository.GetAllFacultiesAsync();

            if (!getResult.IsSuccess)
            {
                return new FacultyServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<FacultyDto> dtoResult = _mapper.Map<IReadOnlyList<FacultyDto>>(getResult.Faculties);

            return new FacultyServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                Faculties = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of faculties from the database.");

            return new FacultyServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve a list of faculties from the database",
                IsSuccess = false
            };
        }
    }
}