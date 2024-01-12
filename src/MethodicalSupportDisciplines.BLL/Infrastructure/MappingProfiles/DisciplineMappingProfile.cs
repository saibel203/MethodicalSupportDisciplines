using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Learning;
using MethodicalSupportDisciplines.Shared.Dto.Learning;
using MethodicalSupportDisciplines.Shared.Responses.Services.LearningServicesResponses;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Learning;
using MethodicalSupportDisciplines.Shared.ViewModels.Learning;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class DisciplineMappingProfile : Profile
{
    public DisciplineMappingProfile()
    {
        CreateMap<DisciplineMaterial, DisciplineMaterialActionDto>().ReverseMap();
        CreateMap<Discipline, DisciplineActionDto>();
        CreateMap<DisciplineServiceResponse, DisciplinesViewModel>();
        
        CreateMap<NewDisciplineDto, Discipline>();
        CreateMap<CreateDisciplineViewModel, NewDisciplineDto>();

        CreateMap<DisciplineGroupActionDto, DisciplineGroup>().ReverseMap();
    }   
}