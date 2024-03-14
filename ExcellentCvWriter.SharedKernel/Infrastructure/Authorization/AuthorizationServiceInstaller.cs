using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Authorization;

/// <summary>
/// Represents the authorization service installer.
/// </summary>
internal sealed class AuthorizationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddAuthorization();
}
