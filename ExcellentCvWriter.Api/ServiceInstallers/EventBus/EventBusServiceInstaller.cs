using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using MassTransit;

namespace ExcellentCvWriter.Api.ServiceInstallers.EventBus;

/// <summary>
/// Represents the event bus service installer
/// </summary>
internal sealed class EventBusServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<MassTransitHostOptionsSetup>();
        services.AddMassTransit(bussConfigurator =>
        {
            bussConfigurator.SetKebabCaseEndpointNameFormatter();

            bussConfigurator.AddConsumersFromAssemblies(
                Modules.Users.Infrastructure.AssemblyReference.Assembly);

            bussConfigurator.AddRequestClientsFromAssemblies(
                SharedKernel.AssemblyReference.Assembly);
            
            bussConfigurator.UsingInMemory((context, configurator) => configurator.ConfigureEndpoints(context));
        });
    }
}