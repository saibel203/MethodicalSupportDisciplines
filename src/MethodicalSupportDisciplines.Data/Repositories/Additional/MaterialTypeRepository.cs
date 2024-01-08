using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class MaterialTypeRepository : RepositoryBase, IMaterialTypeRepository
{
    private readonly ILogger<MaterialTypeRepository> _logger;
    private readonly IStringLocalizer<MaterialTypeRepository> _stringLocalization;

    public MaterialTypeRepository(DataDbContext context, ILogger<MaterialTypeRepository> logger,
        IStringLocalizer<MaterialTypeRepository> stringLocalization)
        : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<MaterialTypeRepositoryResponse> GetMaterialTypesAsync()
    {
        try
        {
            IReadOnlyList<MaterialType> materialTypes = await Context.Set<MaterialType>()
                .ToListAsync();

            return new MaterialTypeRepositoryResponse
            {
                Message = _stringLocalization["SuccessGetMaterialTypes"],
                IsSuccess = true,
                MaterialTypes = materialTypes
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve material types from the database.");

            return new MaterialTypeRepositoryResponse
            {
                Message = _stringLocalization["UnknownErrorGetMaterialTypes"],
                IsSuccess = false
            };
        }
    }
}