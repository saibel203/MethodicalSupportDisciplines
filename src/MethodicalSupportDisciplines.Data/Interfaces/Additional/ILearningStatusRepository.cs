﻿using MethodicalSupportDisciplines.Shared.Responses.Repositories.AdditionalRepositoriesResponses;

namespace MethodicalSupportDisciplines.Data.Interfaces.Additional;

public interface ILearningStatusRepository : IRepositoryBase
{
    Task<LearningStatusRepositoryResponse> GetAllLearningStatusesAsync();
}