using MassTransit;
using Microsoft.Extensions.Options;

namespace ExcellentCvWriter.Api.ServiceInstallers.EventBus;

internal sealed class MassTransitHostOptionsSetup : IConfigureOptions<MassTransitHostOptions>
{
    public void Configure(MassTransitHostOptions options)
    {
        options.WaitUntilStarted = true;
    }
}