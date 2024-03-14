using ExcellentCvWriter.SharedKernel.Infrastructure.Authorization.Attributes;
using Microsoft.AspNetCore.Builder;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Authorization.Extensions;

public static class CustomAuthorizationEndpointConventionBuilderExtensions
{
    public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, string permission)
        where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(
            new HasPermissionAttribute(permission)); });
        return builder;
    }
}