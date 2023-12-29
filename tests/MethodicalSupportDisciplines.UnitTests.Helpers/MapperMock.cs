using AutoMapper;
using MethodicalSupportDisciplines.BLL.Infrastructure.MappingProfiles;

namespace MethodicalSupportDisciplines.UnitTests.Helpers;

public static class MapperMock
{
    private static IMapper? _mapperInstance;

    public static IMapper GetMapper()
    {
        if (_mapperInstance is null)
        {
            _mapperInstance = GetMapperValue();
        }

        return _mapperInstance;
    }

    private static IMapper GetMapperValue()
    {
        return new MapperConfiguration(mapperConfig =>
        {
            mapperConfig.AddProfile(new AuthAutomapperProfile());
            mapperConfig.AddProfile(new FacultyMappingProfile());
            mapperConfig.AddProfile(new FormatLearningMappingProfile());
            mapperConfig.AddProfile(new GroupMappingProfile());
            mapperConfig.AddProfile(new LearningStatusMappingProfile());
            mapperConfig.AddProfile(new QualificationsMappingProfile());
            mapperConfig.AddProfile(new SpecialtyMappingProfile());
            mapperConfig.AddProfile(new UsersAutomapperProfile());
        }).CreateMapper();
    }
}