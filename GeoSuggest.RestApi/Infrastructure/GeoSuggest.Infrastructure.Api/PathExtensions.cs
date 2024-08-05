using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace GeoSuggest.Infrastructure.Api;

public static class PathExtensions
{
    public static IEndpointRouteBuilder UseApiVersionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/version", async context =>
        {
            var version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            await context.Response.WriteAsync(version ?? "");
        });

        return app;
    }
}