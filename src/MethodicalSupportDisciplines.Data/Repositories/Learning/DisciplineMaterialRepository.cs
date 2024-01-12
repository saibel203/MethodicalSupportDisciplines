using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class DisciplineMaterialRepository : RepositoryBase, IDisciplineMaterialRepository
{
    private readonly ILogger<DisciplineMaterialRepository> _logger;
    private readonly IStringLocalizer<DisciplineMaterialRepository> _stringLocalization;

    public DisciplineMaterialRepository(DataDbContext context, ILogger<DisciplineMaterialRepository> logger,
        IStringLocalizer<DisciplineMaterialRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<DisciplineMaterialRepositoryResponse> RemoveDisciplineMaterialAsync(int disciplineMaterialId)
    {
        try
        {
            DisciplineMaterial? disciplineMaterial = await Context.Set<DisciplineMaterial>()
                .Include(disciplineMaterialData => disciplineMaterialData.Materials)
                .ThenInclude(disciplineMaterialData => disciplineMaterialData.Material)
                .AsSplitQuery()
                .FirstOrDefaultAsync(data => data.DisciplineMaterialId == disciplineMaterialId);

            if (disciplineMaterial is null)
            {
                return new DisciplineMaterialRepositoryResponse
                {
                    Message = _stringLocalization["DisciplineMaterialNotFound"],
                    IsSuccess = false
                };
            }

            Context.DisciplineMaterials.Remove(disciplineMaterial);
            await Context.SaveChangesAsync();

            return new DisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["DisciplineMaterialSuccessRemove"],
                IsSuccess = true,
                DisciplineMaterial = disciplineMaterial
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete a class.");

            return new DisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["DisciplineMaterialUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineMaterialRepositoryResponse> CreateDisciplineMaterialAsync(
        DisciplineMaterial disciplineMaterial)
    {
        try
        {
            await Context.DisciplineMaterials.AddAsync(disciplineMaterial);
            await Context.SaveChangesAsync();

            return new DisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["CreateDisciplineMaterialSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create new material for the course.");

            return new DisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["CreateDisciplineUnknownError"],
                IsSuccess = false
            };
        }
    }
}