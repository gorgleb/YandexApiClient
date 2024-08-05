using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoSuggest.Infrastructure.Api.Extensions;

public static class CorsExtensions
{
    private const string CorsPolicyName = "AllowedCors";

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddCors(options => options.AddPolicy(CorsPolicyName, ConfigureCorsPolicy(configuration)));
    }

    public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app)
    {
        return app.UseCors(CorsPolicyName);
    }

    private static Action<CorsPolicyBuilder> ConfigureCorsPolicy(IConfiguration configuration)
    {
        return cors =>
        {
            var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            if (origins?.Any() == true)
            {
                cors.SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        };
    }
}