using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MethodicalSupportDisciplines.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        const string dataDatabaseName = "MethodicalSupportDisciplinesData";

        string? dataDatabaseConnectionString = configuration.GetConnectionString(dataDatabaseName);

        if (dataDatabaseConnectionString is null)
            throw new NullReferenceException("Unable to get database connection string.");

        services.AddDbContext<DataDbContext>(contextOptions =>
            contextOptions.UseSqlServer(dataDatabaseConnectionString));

        return services;
    }
}