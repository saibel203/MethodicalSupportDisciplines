using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class DisciplineMaterialTypeRepository : RepositoryBase, IDisciplineMaterialTypeRepository
{
    private readonly ILogger<DisciplineMaterialTypeRepository> _logger;
    private readonly IStringLocalizer<DisciplineMaterialTypeRepository> _stringLocalization;

    public DisciplineMaterialTypeRepository(DataDbContext context, ILogger<DisciplineMaterialTypeRepository> logger,
        IStringLocalizer<DisciplineMaterialTypeRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<DisciplineMaterialTypeRepositoryResponse> GetDisciplineMaterialTypesAsync()
    {
        try
        {
            IReadOnlyList<DisciplineMaterialType> returnResult = await Context.Set<DisciplineMaterialType>()
                .ToListAsync();

            return new DisciplineMaterialTypeRepositoryResponse
            {
                Message = _stringLocalization["GetDisciplineMaterialTypesSuccess"],
                IsSuccess = true,
                DisciplineMaterialTypes = returnResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown error occurred while trying to get material types.");

            return new DisciplineMaterialTypeRepositoryResponse
            {
                Message = _stringLocalization["GetDisciplineMaterialTypesUnknownError"],
                IsSuccess = false
            };
        }
    }
}