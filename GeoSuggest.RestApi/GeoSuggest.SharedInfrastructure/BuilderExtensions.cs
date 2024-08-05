using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GeoSuggest.SharedInfrastructure;

public static class BuilderExtensions
{
    public static IHostBuilder WithCommonConfig(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;
            config
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("secrets/appsettings.secrets.json", optional: true);

            config.AddEnvironmentVariables();
        });

        hostBuilder.UseContentRoot(Directory.GetCurrentDirectory());

        return hostBuilder;
    }
}