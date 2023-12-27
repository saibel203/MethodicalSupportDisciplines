using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Data.Interfaces.Additional;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MethodicalSupportDisciplines.Data.Repositories.Additional;

public class FormatLearningRepository : RepositoryBase, IFormatLearningRepository
{
    private readonly ILogger<FormatLearningRepository> _logger;

    public FormatLearningRepository(DataDbContext context, ILogger<FormatLearningRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task<FormatLearningRepositoryResponse> GetAllFormatLearningsAsync()
    {
        try
        {
            IReadOnlyList<FormatLearning> formatLearnings = await GetAllReadOnlyList<FormatLearning>();

            return new FormatLearningRepositoryResponse
            {
                Message = "List of format learnings successfully received",
                IsSuccess = true,
                FormatLearnings = formatLearnings
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An unknown error occurred while trying to retrieve a list of format learnings from the database.");

            return new FormatLearningRepositoryResponse
            {
                Message =
                    "An unknown error occurred while trying to retrieve a list of format learnings from the database",
                IsSuccess = false
            };
        }
    }
}