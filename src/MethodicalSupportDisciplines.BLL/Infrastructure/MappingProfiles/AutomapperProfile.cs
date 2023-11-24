using AutoMapper;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.Shared.Dto;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<UserRegisterDto, ApplicationUser>();
        CreateMap<RegisterViewModel, UserRegisterDto>();

        CreateMap<LoginViewModel, UserLoginDto>();
    }
}