using MethodicalSupportDisciplines.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.Seeds;

public class SeedDataDbContext
{
    private readonly DataDbContext _dataDbContext;
    private readonly ILogger<SeedDataDbContext> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SeedDataDbContext(DataDbContext dataDbContext, ILogger<SeedDataDbContext> logger, 
        RoleManager<ApplicationRole> roleManager)
    {
        _dataDbContext = dataDbContext;
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            if (_dataDbContext.Database.IsSqlServer())
                await _dataDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to initialize the database.");
            throw;
        }
    }

    public async Task SeedContextDataAsync()
    {
        try
        {
            await TrySeedDataAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to seed the database.");
            throw;
        }
    }

    private async Task TrySeedDataAsync()
    {
        /* ====================== SEED ROLES ====================== */
        ApplicationRole[] defaultRoles =
        {
            new("admin", "Адмін."),
            new("teacher", "Вчитель."),
            new("student", "Студент."),
            new("guest", "Гість. Роль отримується одразу при реєстрації. Після реєстрації, admin " +
                         "користувач може змінити роль гостя на student або teacher у своєму списку. ")
        };

        foreach (ApplicationRole defaultRole in defaultRoles)
        {
            bool isRoleExist = await _roleManager.RoleExistsAsync(defaultRole.Name!);

            if (!isRoleExist)
                await _roleManager.CreateAsync(defaultRole);
        }
    }
}