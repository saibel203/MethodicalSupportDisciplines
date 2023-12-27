using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class QualificationRepository : RepositoryBase, IQualificationRepository
{
    private readonly ILogger<QualificationRepository> _logger;

    public QualificationRepository(DataDbContext context, ILogger<QualificationRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<QualificationRepositoryResponse> GetAllQualificationsAsync()
    {
        try
        {
            IReadOnlyList<Qualification> qualifications = await GetAllReadOnlyList<Qualification>();

            return new QualificationRepositoryResponse
            {
                Message = "List of qualifications successfully received",
                IsSuccess = true,
                Qualifications = qualifications
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of qualifications from the database.");

            return new QualificationRepositoryResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of qualifications from the database",
                IsSuccess = false
            };
        }
    }
}