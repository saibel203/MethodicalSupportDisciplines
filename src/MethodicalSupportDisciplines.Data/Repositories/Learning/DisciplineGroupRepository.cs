using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class DisciplineGroupRepository : RepositoryBase, IDisciplineGroupRepository
{
    private readonly ILogger<DisciplineGroupRepository> _logger;
    private readonly IStringLocalizer<DisciplineGroupRepository> _stringLocalization;

    public DisciplineGroupRepository(DataDbContext context, ILogger<DisciplineGroupRepository> logger,
        IStringLocalizer<DisciplineGroupRepository> stringLocalization) :
        base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
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
                    Message = _stringLocalization["NotFound"],
                    IsSuccess = false
                };
            }

            Context.DisciplineGroups.Remove(disciplineGroup);
            await Context.SaveChangesAsync();

            return new DisciplineGroupRepositoryResponse
            {
                Message = _stringLocalization["RemoveSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to uninstall.");

            return new DisciplineGroupRepositoryResponse
            {
                Message = _stringLocalization["RemoveUnknownError"],
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
                Message = "Creating success",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to uninstall");

            return new DisciplineGroupRepositoryResponse
            {
                Message = "An unknown error occurred while trying to create",
                IsSuccess = false
            };
        }
    }
}