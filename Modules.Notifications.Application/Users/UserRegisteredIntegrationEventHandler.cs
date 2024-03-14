using ExcellentCvWriter.SharedKernel.Application.EventBus;
using Modules.Users.IntegrationEvents;

namespace Modules.Notifications.Application.Users;

internal sealed class UserRegisteredIntegrationEventHandler : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{

    public UserRegisteredIntegrationEventHandler()
    {
        
    }

    /// <inheritdoc />
    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        // TODO IMPLEMENT
    }
}
