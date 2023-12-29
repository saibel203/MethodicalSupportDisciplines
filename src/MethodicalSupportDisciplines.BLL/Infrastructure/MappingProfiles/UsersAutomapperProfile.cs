using AutoMapper;
using MethodicalSupportDisciplines.Core.Entities.Users;
using MethodicalSupportDisciplines.Shared.Dto.Users;
using MethodicalSupportDisciplines.Shared.ViewModels.Forms.AssignRoles;

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
        
        CreateMap<TeacherUser, GetTeacherUserDto>()
            .ForMember(dest => dest.UserName, opt =>
                opt.MapFrom(src => src.ApplicationUser!.UserName))
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.ApplicationUser!.Email))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.ApplicationUser!.PhoneNumber))
            .ForMember(dest => dest.QualificationName, opt =>
                opt.MapFrom(src => src.Qualification.QualificationName));
        
        CreateMap<StudentUser, GetStudentUserDto>()
            .ForMember(dest => dest.UserName, opt =>
                opt.MapFrom(src => src.ApplicationUser!.UserName))
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.ApplicationUser!.Email))
            .ForMember(dest => dest.PhoneNumber, opt =>
                opt.MapFrom(src => src.ApplicationUser!.PhoneNumber))
            .ForMember(dest => dest.FormatLearningName, opt =>
                opt.MapFrom(src => src.FormatLearning.FormatLearningName))
            .ForMember(dest => dest.LearningStatusName, opt =>
                opt.MapFrom(src => src.LearningStatus.LearningStatusName))
            .ForMember(dest => dest.FacultyName, opt =>
                opt.MapFrom(src => src.Faculty.FacultyName))
            .ForMember(dest => dest.FacultyShortName, opt =>
                opt.MapFrom(src => src.Faculty.FacultyShortName))
            .ForMember(dest => dest.SpecialtyName, opt =>
                opt.MapFrom(src => src.Specialty.SpecialityName))
            .ForMember(dest => dest.SpecialityNumber, opt =>
                opt.MapFrom(src => src.Specialty.SpecialityNumber))
            .ForMember(dest => dest.GroupName, opt =>
                opt.MapFrom(src => src.Group.GroupName))
            .ForMember(dest => dest.GroupCourse, opt =>
                opt.MapFrom(src => src.Group.GroupCourse));

        CreateMap<CreateTeacherViewModel, CreateTeacherDto>();
        CreateMap<CreateStudentViewModel, CreateStudentDto>();
    }
}