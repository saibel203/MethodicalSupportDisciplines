using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class MaterialRepository : RepositoryBase, IMaterialRepository
{
    private readonly ILogger<MaterialRepository> _logger;
    private readonly IStringLocalizer<MaterialRepository> _stringLocalization;

    public MaterialRepository(DataDbContext context, ILogger<MaterialRepository> logger,
        IStringLocalizer<MaterialRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<MaterialRepositoryResponse> CreateMaterialAsync(Material material)
    {
        try
        {
            await Context.Materials.AddAsync(material);
            await Context.SaveChangesAsync();
            int createdMaterialId = material.MaterialId;

            return new MaterialRepositoryResponse
            {
                Message = _stringLocalization["MaterialCreatedSuccess"],
                IsSuccess = true,
                CreatedMaterialId = createdMaterialId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create a new material.");

            return new MaterialRepositoryResponse
            {
                Message = _stringLocalization["MaterialCreatedUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<MaterialRepositoryResponse> RemoveMaterialAsync(int materialId)
    {
        try
        {
            Material? material = await Context.Set<Material>()
                .FirstOrDefaultAsync(materialData => materialData.MaterialId == materialId);

            if (material is null)
            {
                return new MaterialRepositoryResponse
                {
                    Message = _stringLocalization["RemoveMaterialNotFoundMaterial"],
                    IsSuccess = false
                };
            }

            Context.Materials.Remove(material);
            await Context.SaveChangesAsync();

            return new MaterialRepositoryResponse
            {
                Message = _stringLocalization["RemoveMaterialSuccess"],
                IsSuccess = true,
                Material = material
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to delete material from the database.");

            return new MaterialRepositoryResponse
            {
                Message = _stringLocalization["RemoveMaterialUnknownError"],
                IsSuccess = false
            };
        }
    }
}