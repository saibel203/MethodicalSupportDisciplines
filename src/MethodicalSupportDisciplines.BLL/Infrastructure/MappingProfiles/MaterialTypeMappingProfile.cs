using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.AdditionalLearning;
using MethodicalSupportDisciplines.Shared.Dto.Additional;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class MaterialTypeMappingProfile : Profile
{
    public MaterialTypeMappingProfile()
    {
        CreateMap<MaterialType, MaterialTypeDto>();
    }
}