using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.Seeds;

public class SeedDataDbContext
{
    private readonly DataDbContext _dataDbContext;
    private readonly ILogger<SeedDataDbContext> _logger;

    public SeedDataDbContext(DataDbContext dataDbContext, ILogger<SeedDataDbContext> logger)
    {
        _dataDbContext = dataDbContext;
        _logger = logger;
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
        
    }
}