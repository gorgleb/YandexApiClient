using Mapster;

namespace GeoSuggest.RestApi.Mapping;

public static class MappingExtensions
{
    public static void AddRestApiMappings(this IServiceCollection services)
    {
        services.AddSingleton(ConfigureWebMappings());
    }

    public static TypeAdapterConfig ConfigureWebMappings()
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Apply(typeAdapterConfig.Scan(typeof(MappingExtensions).Assembly));

        return typeAdapterConfig;
    }
}