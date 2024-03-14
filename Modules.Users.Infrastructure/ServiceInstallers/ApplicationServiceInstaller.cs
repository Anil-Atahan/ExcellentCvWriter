using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using ExcellentCvWriter.SharedKernel.Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Infrastructure.Idempotence;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module application service installer.
/// </summary>
internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly));
        DecorateDomainEventHandlersWithIdempotency(services);
        AddAndDecorateIntegrationEventHandlersWithIdempotency(services);
    }

    private static void DecorateDomainEventHandlersWithIdempotency(IServiceCollection services)
    {
        var types = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(EventHandlersUtility.ImplementsDomainEventHandler);

        foreach (var type in types)
        {
            Type closedNotificationHandler = type.GetInterfaces().First(EventHandlersUtility.IsNotificationHandler);

            Type[] arguments = closedNotificationHandler.GetGenericArguments();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(arguments);

            services.Decorate(closedNotificationHandler, closedIdempotentHandler);
        }
    }

    private static void AddAndDecorateIntegrationEventHandlersWithIdempotency(IServiceCollection services)
    {
        var types = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(EventHandlersUtility.ImplementsIntegrationEventHandler);

        foreach (var type in types)
        {
            Type closedIntegrationEventHandler = type
                .GetInterfaces()
                .First(EventHandlersUtility.IsIntegrationEventHandler);

            Type[] arguments = closedIntegrationEventHandler.GetGenericArguments();

            Type closedIdempotentHandler = typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(arguments);

            services.AddScoped(type);

            services.Decorate(type, closedIdempotentHandler);
        }
    }
}
