using ExcellentCvWriter.SharedKernel.Application.EventBus;
using ExcellentCvWriter.SharedKernel.Application.Time;
using ExcellentCvWriter.SharedKernel.Infrastructure.EventBus;
using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using ExcellentCvWriter.SharedKernel.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module infrastructure service installer.
/// </summary>
internal sealed class InfrastructureServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddTransient<ISystemTime, SystemTime>();
        services.TryAddTransient<IEventBus, EventBus>();
    }
}
