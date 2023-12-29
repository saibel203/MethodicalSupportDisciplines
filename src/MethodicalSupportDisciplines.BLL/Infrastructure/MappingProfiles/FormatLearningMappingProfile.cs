using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class FormatLearningMappingProfile : Profile
{
    public FormatLearningMappingProfile()
    {
        CreateMap<FormatLearning, FormatLearningDto>();
    }
}