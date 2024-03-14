using MediatR;
using Microsoft.AspNetCore.Routing;

namespace ExcellentCvWriter.SharedKernel.Endpoints;

public abstract class EndpointBase
{
    public abstract IEndpointRouteBuilder AddEndpoint(IEndpointRouteBuilder routeGroupBuilder);
    public static class WithRequest<TRequest>
    {
        public abstract class WithResult<TResponse> : EndpointBase
        {
            public abstract Task<TResponse> HandleAsync(TRequest request, ISender sender, CancellationToken cancellationToken);
        }

        public abstract class WithoutResult : EndpointBase
        {
            public abstract Task HandleAsync(TRequest request, ISender sender, CancellationToken cancellationToken);
        }
    }

    public static class WithoutRequest
    {
        public abstract class WithResult<TResponse> : EndpointBase
        {
            public abstract Task<TResponse> HandleAsync(ISender sender, CancellationToken cancellationToken);
        }

        public abstract class WithoutResult : EndpointBase
        {
            public abstract Task HandleAsync(ISender sender, CancellationToken cancellationToken);
        }
    }
}