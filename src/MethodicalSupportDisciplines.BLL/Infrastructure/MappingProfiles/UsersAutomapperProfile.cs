using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Shared.Dto.Users;

namespace MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

public class UsersAutomapperProfile : Profile
{
    public UsersAutomapperProfile()
    {
        CreateMap<GuestUser, GetGuestUserDto>()
            .ForMember(dest => dest.UserName, opt =>
                opt.MapFrom(src => src.ApplicationUser!.UserName))
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.ApplicationUser!.Email))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.ApplicationUser!.PhoneNumber));
    }
}