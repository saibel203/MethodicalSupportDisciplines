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
        }).CreateMapper();
    }
}