using AutoMapper;
using MethodicalSupportDisciplines.BLL.Helpers;
using MethodicalSupportDisciplines.BLL.Interfaces.Learning;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Shared.AdditionalModels;
using MethodicalSupportDisciplines.Shared.Constants;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Learning;

public class DisciplineService : BaseService<IDisciplineRepository>, IDisciplineService
{
    private readonly ILogger<DisciplineService> _logger;
    private readonly IStringLocalizer<DisciplineService> _stringLocalization;

    public DisciplineService(IDisciplineRepository repository, IMapper mapper, ILogger<DisciplineService> logger,
        IStringLocalizer<DisciplineService> stringLocalization) :
        base(repository, mapper)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<DisciplineServiceResponse> GetAllDisciplinesAsync(QueryParameters queryParameters,
        string applicationUserId)
    {
        try
        {
            DisciplineRepositoryResponse getDisciplinesResult =
                await _repository.GetAllDisciplinesAsync(applicationUserId);

            if (!getDisciplinesResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getDisciplinesResult.Message,
                    IsSuccess = false
                };
            }

            int skipAmount = PagesParameters.DisciplineCardsCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<Discipline> disciplines = getDisciplinesResult.Disciplines;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                disciplines = SearchHelper.ReadOnlySearch(disciplineData =>
                    disciplineData.DisciplineName.Contains(queryParameters.SearchString,
                        StringComparison.CurrentCultureIgnoreCase) ||
                    disciplineData.DisciplineDescription.Contains(queryParameters
                        .SearchString, StringComparison.CurrentCultureIgnoreCase), disciplines);
            }

            int guestUsersCount = disciplines.Count;
            int pageCount = (int)Math.Ceiling((double)guestUsersCount / PagesParameters.DisciplineCardsCount);

            IReadOnlyList<DisciplineActionDto> dtoResult =
                _mapper.Map<IReadOnlyList<DisciplineActionDto>>(disciplines);

            return new DisciplineServiceResponse
            {
                Message = getDisciplinesResult.Message,
                IsSuccess = true,
                Disciplines = dtoResult
                    .Skip(skipAmount)
                    .Take(PagesParameters.DisciplineCardsCount)
                    .ToList(),
                SearchString = queryParameters.SearchString,
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve the list of disciplines.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve the list of disciplines",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> GetAllDisciplinesForAdminAsync(QueryParameters queryParameters)
    {
        try
        {
            DisciplineRepositoryResponse getDisciplinesResult = await _repository.GetAllDisciplinesForAdminAsync();

            if (!getDisciplinesResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getDisciplinesResult.Message,
                    IsSuccess = false
                };
            }

            int skipAmount = PagesParameters.DisciplineCardsCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<Discipline> disciplines = getDisciplinesResult.Disciplines;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                disciplines = SearchHelper.ReadOnlySearch(disciplineData =>
                    disciplineData.DisciplineName.Contains(queryParameters.SearchString,
                        StringComparison.CurrentCultureIgnoreCase) ||
                    disciplineData.DisciplineDescription.Contains(queryParameters
                        .SearchString, StringComparison.CurrentCultureIgnoreCase) ||
                    disciplineData.Teacher.FirstName.Contains(queryParameters.SearchString) ||
                    disciplineData.Teacher.LastName.Contains(queryParameters.SearchString) ||
                    disciplineData.Teacher.Patronymic.Contains(queryParameters.SearchString), disciplines);
            }

            int guestUsersCount = disciplines.Count;
            int pageCount = (int)Math.Ceiling((double)guestUsersCount / PagesParameters.DisciplineCardsCount);

            IReadOnlyList<DisciplineActionDto> dtoResult =
                _mapper.Map<IReadOnlyList<DisciplineActionDto>>(disciplines);

            return new DisciplineServiceResponse
            {
                Message = getDisciplinesResult.Message,
                IsSuccess = true,
                Disciplines = dtoResult
                    .Skip(skipAmount)
                    .Take(PagesParameters.DisciplineCardsCount)
                    .ToList(),
                SearchString = queryParameters.SearchString,
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve the list of disciplines.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve the list of disciplines",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId)
    {
        try
        {
            DisciplineRepositoryResponse getDisciplineResult =
                await _repository.GetDisciplineByIdAsync(disciplineId, currentUserId);

            if (!getDisciplineResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getDisciplineResult.Message,
                    IsSuccess = false
                };
            }

            DisciplineActionDto dtoResult = _mapper.Map<DisciplineActionDto>(getDisciplineResult.Discipline);

            return new DisciplineServiceResponse
            {
                Message = getDisciplineResult.Message,
                IsSuccess = true,
                Discipline = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to obtain discipline.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to obtain discipline",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> GetDisciplineForStudentByIdAsync(int disciplineId, string userId)
    {
        try
        {
            DisciplineRepositoryResponse getResponse = 
                await _repository.GetDisciplineForStudentByIdAsync(disciplineId, userId);

            if (!getResponse.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getResponse.Message,
                    IsSuccess = false
                };
            }
            
            DisciplineActionDto dtoResult = _mapper.Map<DisciplineActionDto>(getResponse.Discipline);

            return new DisciplineServiceResponse
            {
                Message = getResponse.Message,
                IsSuccess = true,
                Discipline = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to obtain discipline.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to obtain discipline",
                IsSuccess = false
            };
        }
    }
    
    public async Task<DisciplineServiceResponse> GetDisciplineForAdminByIdAsync(int disciplineId)
    {
        try
        {
            DisciplineRepositoryResponse getDisciplineResult =
                await _repository.GetDisciplineForAdminByIdAsync(disciplineId);

            if (!getDisciplineResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getDisciplineResult.Message,
                    IsSuccess = false
                };
            }

            DisciplineActionDto dtoResult = _mapper.Map<DisciplineActionDto>(getDisciplineResult.Discipline);

            return new DisciplineServiceResponse
            {
                Message = getDisciplineResult.Message,
                IsSuccess = true,
                Discipline = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to obtain discipline.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to obtain discipline",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> GetDisciplinesForStudentAsync(string userId, 
        QueryParameters queryParameters)
    {
        try
        {
            DisciplineRepositoryResponse getDisciplinesResponse = 
                await _repository.GetDisciplinesForStudentGroup(userId);

            if (!getDisciplinesResponse.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = getDisciplinesResponse.Message,
                    IsSuccess = false
                };
            }
            
            int skipAmount = PagesParameters.DisciplineCardsCount * (queryParameters.PageNumber - 1);

            IReadOnlyList<DisciplineGroup> disciplineGroups = getDisciplinesResponse.DisciplineGroups;

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchString))
            {
                disciplineGroups = SearchHelper.ReadOnlySearch(disciplineData =>
                    disciplineData.Discipline.DisciplineName.Contains(queryParameters.SearchString,
                        StringComparison.CurrentCultureIgnoreCase) ||
                    disciplineData.Discipline.DisciplineDescription.Contains(queryParameters
                        .SearchString, StringComparison.CurrentCultureIgnoreCase), disciplineGroups);
            }

            int guestUsersCount = disciplineGroups.Count;
            int pageCount = (int)Math.Ceiling((double)guestUsersCount / PagesParameters.DisciplineCardsCount);
            
            IReadOnlyList<DisciplineGroupActionDto> dtoResult = 
                _mapper.Map<IReadOnlyList<DisciplineGroupActionDto>>(disciplineGroups);

            return new DisciplineServiceResponse
            {
                Message = getDisciplinesResponse.Message,
                IsSuccess = true,
                DisciplineGroups = dtoResult.Skip(skipAmount)
                    .Take(PagesParameters.DisciplineCardsCount)
                    .ToList(),
                SearchString = queryParameters.SearchString,
                PageCount = pageCount,
                ItemsCount = guestUsersCount,
                Pages = PaginationHelper.PageNumbers(queryParameters.PageNumber, pageCount)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to retrieve the list of disciplines.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve the list of disciplines",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> CreateDisciplineAsync(NewDisciplineDto? dto)
    {
        try
        {
            if (dto is null)
            {
                return new DisciplineServiceResponse
                {
                    Message = _stringLocalization["MethodGetIncorrectData"],
                    IsSuccess = false
                };
            }

            Discipline discipline = _mapper.Map<Discipline>(dto);

            DisciplineRepositoryResponse createResult = await _repository.CreateDisciplineAsync(discipline);

            if (!createResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = createResult.Message,
                    IsSuccess = false
                };
            }

            return new DisciplineServiceResponse
            {
                Message = createResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create a new discipline.");

            return new DisciplineServiceResponse
            {
                Message = "An unknown error occurred while trying to create a new discipline",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineServiceResponse> RemoveDisciplineAsync(int disciplineId)
    {
        try
        {
            DisciplineRepositoryResponse removeResult =
                await _repository.RemoveDisciplineAsync(disciplineId);

            if (!removeResult.IsSuccess)
            {
                return new DisciplineServiceResponse
                {
                    Message = removeResult.Message,
                    IsSuccess = false
                };
            }

            return new DisciplineServiceResponse
            {
                Message = removeResult.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve and delete a discipline from the database.");

            return new DisciplineServiceResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve and delete a discipline from the database",
                IsSuccess = false
            };
        }
    }
}