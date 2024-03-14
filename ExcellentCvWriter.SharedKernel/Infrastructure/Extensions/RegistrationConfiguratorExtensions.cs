using System.Reflection;
using ExcellentCvWriter.SharedKernel.Infrastructure.EventBus;
using MassTransit;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;

public static class RegistrationConfiguratorExtensions
{
    public static void AddConsumersFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies)
    {
        var consumerInstallers = InstanceFactory
            .CreateFromAssemblies<IConsumerConfiguration>(assemblies);

        foreach (var consumerInstaller in consumerInstallers)
        {
            consumerInstaller.AddConsumers(registrationConfigurator);
        }
    }
    
    public static void AddRequestClientsFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) 
    {
        var consumerInstallers = InstanceFactory
            .CreateFromAssemblies<IRequestClientConfiguration>(assemblies);

        foreach (var consumerInstaller in consumerInstallers)
        {
            consumerInstaller.AddRequestClients(registrationConfigurator);
        }
    }
}