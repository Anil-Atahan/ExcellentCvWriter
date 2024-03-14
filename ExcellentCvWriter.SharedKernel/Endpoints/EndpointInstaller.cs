using System.Reflection;
using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using Microsoft.AspNetCore.Routing;

namespace ExcellentCvWriter.SharedKernel.Endpoints;

/// <summary>
/// Represents the endpoint installer.
/// </summary>
public static class EndpointInstaller
{
    /// <inheritdoc />
    public static IEndpointRouteBuilder InstallEndpoints(
        this IEndpointRouteBuilder routeGroupBuilder,
        params Assembly[] assemblies)
    {
        var endpoints = InstanceFactory
            .CreateFromAssemblies<EndpointBase>(assemblies);
        foreach (var endpoint in endpoints)
        {
            endpoint.AddEndpoint(routeGroupBuilder);
        }

        return routeGroupBuilder;
    }
}


