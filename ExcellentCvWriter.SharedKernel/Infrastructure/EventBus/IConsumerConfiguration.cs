using MassTransit;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.EventBus;

public interface IConsumerConfiguration
{
    void AddConsumers(IRegistrationConfigurator registrationConfigurator);
}