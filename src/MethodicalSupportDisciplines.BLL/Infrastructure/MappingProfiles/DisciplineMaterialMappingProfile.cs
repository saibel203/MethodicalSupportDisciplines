using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class DisciplineMaterialMappingProfile : Profile
{
    public DisciplineMaterialMappingProfile()
    {
        CreateMap<DisciplineMaterial, NewDisciplineMaterialDto>().ReverseMap();
        CreateMap<CreateDisciplineMaterialViewModel, NewDisciplineMaterialDto>();
    }
}