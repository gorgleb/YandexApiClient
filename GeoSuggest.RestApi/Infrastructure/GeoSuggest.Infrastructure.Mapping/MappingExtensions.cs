using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.Infrastructure.Mapping;

public static class MappingExtensions
{
    public static void AddMappings(this IServiceCollection services)
    {
        services.AddSingleton(ConfigureMappings());
    }

    public static TypeAdapterConfig ConfigureMappings()
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Apply(typeAdapterConfig.Scan(typeof(MappingExtensions).Assembly));

        return typeAdapterConfig;
    }
}