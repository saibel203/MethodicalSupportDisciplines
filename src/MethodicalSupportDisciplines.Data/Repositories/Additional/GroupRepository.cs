using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class GroupRepository : RepositoryBase, IGroupRepository
{
    private readonly ILogger<GroupRepository> _logger;

    public GroupRepository(DataDbContext context, ILogger<GroupRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<GroupRepositoryResponse> GetAllGroupsAsync()
    {
        try
        {
            IReadOnlyList<Group> groups = 
                await GetAllReadOnlyListSorting<Group, int>(group => group.GroupCourse);

            return new GroupRepositoryResponse
            {
                Message = "The list of groups was successfully retrieved",
                IsSuccess = true,
                Groups = groups
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of groups from the database.");

            return new GroupRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve a list of groups from the database",
                IsSuccess = false
            };
        }
    }

    /*public async Task<GroupRepositoryResponse> GetAllGroupsForDisciplineAsync(int disciplineId)
    {
        try
        {
            IReadOnlyList<Group> groups = await Context.Set<Group>()
                .Include(x => x.DisciplineGroups)
                .ThenInclude(x => x.Discipline)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new GroupRepositoryResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }*/
}