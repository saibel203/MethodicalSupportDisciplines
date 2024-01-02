using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Data.Interfaces.Learning;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.LearningRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Learning;

public class DisciplineRepository : RepositoryBase, IDisciplineRepository
{
    private readonly ILogger<DisciplineRepository> _logger;
    private readonly IStringLocalizer<DisciplineRepository> _stringLocalization;

    public DisciplineRepository(DataDbContext context, ILogger<DisciplineRepository> logger,
        IStringLocalizer<DisciplineRepository> stringLocalization) : base(context)
    {
        _logger = logger;
        _stringLocalization = stringLocalization;
    }

    public async Task<DisciplineRepositoryResponse> GetAllDisciplinesAsync(string applicationUserId)
    {
        try
        {
            IReadOnlyList<Discipline> disciplines = await Context.Set<Discipline>()
                .Include(disciplineData => disciplineData.Teacher)
                .ThenInclude(teacherData => teacherData.ApplicationUser)
                .Include(disciplineData => disciplineData.DisciplineMaterials)
                .Where(disciplineData => disciplineData.Teacher.ApplicationUser!.Id == applicationUserId)
                .ToListAsync();

            return new DisciplineRepositoryResponse
            {
                Message = "List of disciplines successfully received",
                IsSuccess = true,
                Disciplines = disciplines
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve information about available disciplines from the database.");

            return new DisciplineRepositoryResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve information about available disciplines from the database",
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineRepositoryResponse> GetDisciplineByIdAsync(int disciplineId, string currentUserId)
    {
        try
        {
            Discipline? discipline = await Context.Set<Discipline>()
                .Include(disciplineData => disciplineData.Teacher)
                .Include(disciplineData => disciplineData.DisciplineMaterials)
                .FirstOrDefaultAsync(disciplineData => disciplineData.DisciplineId == disciplineId);

            if (discipline is null)
            {
                return new DisciplineRepositoryResponse
                {
                    Message = _stringLocalization["DisciplineNotFound"],
                    IsSuccess = false
                };
            }

            if (discipline.Teacher.ApplicationUserId != currentUserId)
            {
                return new DisciplineRepositoryResponse
                {
                    Message = _stringLocalization["NotDisciplineOwner"],
                    IsSuccess = false
                };
            }

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["DisciplineGetSuccess"],
                IsSuccess = true,
                Discipline = discipline
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a discipline with the specified ID.");

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["DisciplineGetUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineRepositoryResponse> CreateDisciplineAsync(Discipline discipline)
    {
        try
        {
            await Context.Disciplines.AddAsync(discipline);
            await Context.SaveChangesAsync();

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["CreateSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unknown error occurred while trying to create a new discipline.");

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["CreateUnknownError"],
                IsSuccess = false
            };
        }
    }
}