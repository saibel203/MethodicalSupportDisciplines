using AutoMapper;
using MethodicalSupportDisciplines.BLL.Models.Identity;
using MethodicalSupportDisciplines.Shared.Dto.AuthDto;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.Auth;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<UserRegisterDto, ApplicationUser>();
        CreateMap<RegisterViewModel, UserRegisterDto>();

        CreateMap<LoginViewModel, UserLoginDto>();

        CreateMap<RemindPasswordViewModel, RemindPasswordDto>();
        CreateMap<ResetPasswordViewModel, ResetPasswordDto>()
            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(
                    src => src.Value));
    }
}