using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class FacultyMappingProfile : Profile
{
    public FacultyMappingProfile()
    {
        CreateMap<Faculty, FacultyDto>();
    }
}