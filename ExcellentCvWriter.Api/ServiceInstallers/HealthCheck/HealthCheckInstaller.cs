using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;

namespace ExcellentCvWriter.Api.ServiceInstallers.HealthCheck;

internal sealed class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!)
            .AddUrlGroup(new Uri(configuration["Keycloak:AdminUrl"]!), HttpMethod.Get, "keycloak");
    }
}