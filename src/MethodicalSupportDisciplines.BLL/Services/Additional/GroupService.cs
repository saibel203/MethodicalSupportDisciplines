using AutoMapper;
using MethodicalSupportDisciplines.BLL.Interfaces.Additional;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Shared.Dto.Additional;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using MethodicalSupportDisciplines.Shared.Responses.Services.AdditionalServicesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.BLL.Services.Additional;

public class GroupService : BaseService<IGroupRepository>, IGroupService
{
    private readonly ILogger<GroupService> _logger;

    public GroupService(IGroupRepository repository, IMapper mapper, ILogger<GroupService> logger) : base(repository,
        mapper)
    {
        _logger = logger;
    }

    public async Task<GroupServiceResponse> GetAllGroupsAsync()
    {
        try
        {
            GroupRepositoryResponse getResult = await _repository.GetAllGroupsAsync();

            if (!getResult.IsSuccess)
            {
                return new GroupServiceResponse
                {
                    Message = getResult.Message,
                    IsSuccess = false
                };
            }

            IReadOnlyList<GroupDto> dtoResult = _mapper.Map<IReadOnlyList<GroupDto>>(getResult.Groups);

            return new GroupServiceResponse
            {
                Message = getResult.Message,
                IsSuccess = true,
                Groups = dtoResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of groups from the database.");

            return new GroupServiceResponse
            {
                Message = "An unknown error occurred while trying to retrieve a list of groups from the database",
                IsSuccess = false
            };
        }
    }
}