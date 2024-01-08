using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class MaterialMappingProfile : Profile
{
    public MaterialMappingProfile()
    {
        CreateMap<NewMaterialDto, Material>();
        CreateMap<CreateDisciplineMaterialViewModel, NewMaterialDto>();
    }
}