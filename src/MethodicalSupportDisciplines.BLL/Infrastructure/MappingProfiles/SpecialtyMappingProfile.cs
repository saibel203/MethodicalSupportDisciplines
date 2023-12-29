using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class SpecialtyMappingProfile : Profile
{
    public SpecialtyMappingProfile()
    {
        CreateMap<Specialty, SpecialityDto>();
    }
}