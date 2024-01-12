using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class DisciplineGroupMappingProfile : Profile
{
    public DisciplineGroupMappingProfile()
    {
        CreateMap<DisciplineGroup, NewDisciplineGroupDto>().ReverseMap();
        CreateMap<NewDisciplineGroupDto, CreateDisciplineGroupViewModel>().ReverseMap();
    }
}