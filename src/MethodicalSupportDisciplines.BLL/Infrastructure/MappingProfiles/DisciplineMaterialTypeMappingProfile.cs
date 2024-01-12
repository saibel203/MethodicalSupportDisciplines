using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class DisciplineMaterialTypeMappingProfile : Profile
{
    public DisciplineMaterialTypeMappingProfile()
    {
        CreateMap<DisciplineMaterialType, DisciplineMaterialTypeDto>();
    }
}