using ExcellentCvWriter.Api.OpenApi;
using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;

namespace ExcellentCvWriter.Api.ServiceInstallers.Swagger;

/// <summary>
/// Represents the swagger service installer.
/// </summary>
internal sealed class SwaggerServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    void IServiceInstaller.Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddSwaggerGen();
    }
}
