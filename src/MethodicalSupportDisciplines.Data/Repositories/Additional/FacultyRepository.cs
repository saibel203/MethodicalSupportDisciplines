using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class FacultyRepository : RepositoryBase, IFacultyRepository
{
    private readonly ILogger<FacultyRepository> _logger;

    public FacultyRepository(DataDbContext context, ILogger<FacultyRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<FacultyRepositoryResponse> GetAllFacultiesAsync()
    {
        try
        {
            IReadOnlyList<Faculty> faculties = await GetAllReadOnlyList<Faculty>();

            return new FacultyRepositoryResponse
            {
                Message = "The list of faculties was successfully retrieved",
                IsSuccess = true,
                Faculties = faculties
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of faculties from the database.");

            return new FacultyRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve a list of faculties from the database",
                IsSuccess = false
            };
        }
    }
}