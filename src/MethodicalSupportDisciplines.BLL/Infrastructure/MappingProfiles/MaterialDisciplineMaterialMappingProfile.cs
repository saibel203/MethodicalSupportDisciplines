using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class MaterialDisciplineMaterialMappingProfile : Profile
{
    public MaterialDisciplineMaterialMappingProfile()
    {
        CreateMap<NewMaterialDisciplineMaterialDto, MaterialDisciplineMaterial>();
    }
}