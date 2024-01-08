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
                .OrderByDescending(disciplineData => disciplineData.DisciplineId)
                .ToListAsync();

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["DisciplinesSuccess"],
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
                Message = _stringLocalization["DisciplinesUnknownError"],
                IsSuccess = false
            };
        }
    }

    public async Task<DisciplineRepositoryResponse> GetAllDisciplinesForAdminAsync()
    {
        try
        {
            IReadOnlyList<Discipline> disciplines = await Context.Set<Discipline>()
                .Include(disciplineData => disciplineData.Teacher)
                .ThenInclude(teacherData => teacherData.ApplicationUser)
                .Include(disciplineData => disciplineData.DisciplineMaterials)
                .OrderByDescending(disciplineData => disciplineData.DisciplineId)
                .ToListAsync();

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["DisciplinesForAdminSuccess"],
                IsSuccess = true,
                Disciplines = disciplines
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve information about disciplines from the database.");

            return new DisciplineRepositoryResponse
            {
                Message =
                    _stringLocalization["DisciplinesForAdminUnknownError"],
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

    public async Task<DisciplineRepositoryResponse> GetDisciplineForAdminByIdAsync(int disciplineId)
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

    public async Task<DisciplineRepositoryResponse> RemoveDisciplineAsync(int disciplineId)
    {
        try
        {
            Discipline? discipline = await Context.Set<Discipline>()
                .FirstOrDefaultAsync(disciplineData => disciplineData.DisciplineId == disciplineId);

            if (discipline is null)
            {
                return new DisciplineRepositoryResponse
                {
                    Message = _stringLocalization["DisciplineNotFound"],
                    IsSuccess = false
                };
            }

            Context.Disciplines.Remove(discipline);
            await Context.SaveChangesAsync();

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["RemoveDisciplineSuccess"],
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve and delete a discipline from the database.");

            return new DisciplineRepositoryResponse
            {
                Message = _stringLocalization["RemoveDisciplineUnknownError"],
                IsSuccess = false
            };
        }
    }
}