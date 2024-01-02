using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class SpecialityRepository : RepositoryBase, ISpecialityRepository
{
    private readonly ILogger<SpecialityRepository> _logger;

    public SpecialityRepository(DataDbContext context, ILogger<SpecialityRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<SpecialityRepositoryResponse> GetAllSpecialitiesAsync()
    {
        try
        {
            IReadOnlyList<Speciality> specialties = 
                await GetAllReadOnlyListSorting<Speciality, int>(specialty => specialty.SpecialityNumber);

            return new SpecialityRepositoryResponse
            {
                Message = "The list of specialities was successfully retrieved",
                IsSuccess = true,
                Specialties = specialties
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of specialities from the database.");

            return new SpecialityRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve a list of specialities from the database",
                IsSuccess = false
            };
        }
    }
}