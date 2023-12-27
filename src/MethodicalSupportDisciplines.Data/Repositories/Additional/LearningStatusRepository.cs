using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class LearningStatusRepository : RepositoryBase, ILearningStatusRepository
{
    private readonly ILogger<LearningStatusRepository> _logger;

    public LearningStatusRepository(DataDbContext context, ILogger<LearningStatusRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<LearningStatusRepositoryResponse> GetAllLearningStatusesASync()
    {
        try
        {
            IReadOnlyList<LearningStatus> learningStatuses = await GetAllReadOnlyList<LearningStatus>();

            return new LearningStatusRepositoryResponse
            {
                Message = "The list of learning statuses was successfully retrieved",
                IsSuccess = true,
                LearningStatuses = learningStatuses
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of learning statuses from the database.");

            return new LearningStatusRepositoryResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of learning statuses from the database",
                IsSuccess = false
            };
        }
    }
}