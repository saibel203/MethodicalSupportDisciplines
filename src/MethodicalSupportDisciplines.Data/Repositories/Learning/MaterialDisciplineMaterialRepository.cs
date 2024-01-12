using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class MaterialDisciplineMaterialRepository : RepositoryBase, IMaterialDisciplineMaterialRepository
{
    private readonly ILogger<MaterialDisciplineMaterialRepository> _logger;
    private readonly IStringLocalizer<MaterialDisciplineMaterialRepository> _stringLocalization;

    public MaterialDisciplineMaterialRepository(DataDbContext context,
        ILogger<MaterialDisciplineMaterialRepository> logger,
        IStringLocalizer<MaterialDisciplineMaterialRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<MaterialDisciplineMaterialRepositoryResponse> CreateMaterialDisciplineMaterialAsync(
        MaterialDisciplineMaterial materialDisciplineMaterial)
    {
        try
        {
            await Context.MaterialDisciplineMaterials.AddAsync(materialDisciplineMaterial);
            await Context.SaveChangesAsync();
            
            return new MaterialDisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["SuccessAddRelationship"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "");

            return new MaterialDisciplineMaterialRepositoryResponse
            {
                Message = _stringLocalization["UnknownErrorAddRelationship"],
                IsSuccess = false
            };
        }
    }
}