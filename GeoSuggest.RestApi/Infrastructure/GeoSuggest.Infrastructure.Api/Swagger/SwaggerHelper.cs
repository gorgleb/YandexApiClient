using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GeoSuggest.Infrastructure.Api.Swagger;

public static class SwaggerHelper
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, Action<SwaggerGenOptions> configure = null)
    {
        services.AddEndpointsApiExplorer();
        ConfigureApiVersioning(services);
        ConfigureSwaggerGen(services, configuration, configure);
        return services;
    }

    public static IApplicationBuilder UseSwaggerUi(this IApplicationBuilder builder, IApiVersionDescriptionProvider versioningProvider)
    {
        return builder.UseSwaggerUI(options => ConfigureSwaggerUI(options, versioningProvider));
    }

    private static void ConfigureApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    private static void ConfigureSwaggerGen(IServiceCollection services, IConfiguration configuration, Action<SwaggerGenOptions> configure)
    {
        services.AddSwaggerGen(options =>
        {
            ConfigureSwaggerDocs(services, options, configuration);
            ConfigureSchemaMapping(options);
            ConfigureXmlComments(options);
            configure?.Invoke(options);
        });
    }

    private static void ConfigureSwaggerDocs(IServiceCollection services, SwaggerGenOptions options, IConfiguration configuration)
    {
        var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateOpenApiInfo(description, configuration));
        }
    }

    private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description, IConfiguration configuration)
    {
        return new OpenApiInfo
        {
            Title = configuration.GetSection("Swagger")["ApplicationName"],
            Version = description.ApiVersion.ToString(),
            Description = $"<p><strong>Build: </strong>{GetBuildVersion()}</p>"
        };
    }

    private static string GetBuildVersion()
    {
        return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
    }

    private static void ConfigureSchemaMapping(SwaggerGenOptions options)
    {
        options.MapType<Guid>(() => new OpenApiSchema { Type = "string", Format = "uuid" });
        options.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "" });
        options.MapType<decimal?>(() => new OpenApiSchema { Type = "number", Format = "" });
        options.CustomSchemaIds(type => type.ToString());
        options.UseDateOnlyTimeOnlyStringConverters();
        options.UseAllOfToExtendReferenceSchemas();
        options.EnableAnnotations(true, true);
    }

    private static void ConfigureXmlComments(SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }

    private static void ConfigureSwaggerUI(SwaggerUIOptions options, IApiVersionDescriptionProvider versioningProvider)
    {
        foreach (var description in versioningProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }

        options.DisplayOperationId();
        options.EnableDeepLinking();
    }
}