using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class DisciplineGroupRepository : RepositoryBase, IDisciplineGroupRepository
{
    private readonly ILogger<DisciplineGroupRepository> _logger;

    public DisciplineGroupRepository(DataDbContext context, ILogger<DisciplineGroupRepository> logger) :
        base(context)
    {
        _logger = logger;
    }

    public async Task<DisciplineGroupRepositoryResponse> RemoveDisciplineGroupAsync(int disciplineId, int groupId)
    {
        try
        {
            DisciplineGroup? disciplineGroup = await Context.Set<DisciplineGroup>()
                .FirstOrDefaultAsync(dgd => dgd.DisciplineId == disciplineId && dgd.GroupId == groupId);

            if (disciplineGroup is null)
            {
                return new DisciplineGroupRepositoryResponse
                {
                    Message = "Discipline not found",
                    IsSuccess = false
                };
            }

            Context.DisciplineGroups.Remove(disciplineGroup);
            await Context.SaveChangesAsync();
            
            return new DisciplineGroupRepositoryResponse
            {
                Message = "Remove success",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown delete error");

            return new DisciplineGroupRepositoryResponse
            {
                Message = "Unknown delete error",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineGroupRepositoryResponse> CreateDisciplineGroupAsync(DisciplineGroup disciplineGroup)
    {
        try
        {
            await Context.DisciplineGroups.AddAsync(disciplineGroup);
            await Context.SaveChangesAsync();

            return new DisciplineGroupRepositoryResponse
            {
                Message = "",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new DisciplineGroupRepositoryResponse
            {
                Message = "",
                IsSuccess = false
            };
        }
    }
}