using ExcellentCvWriter.SharedKernel.Common.Errors;
using ExcellentCvWriter.SharedKernel.Common.Results;
using ExcellentCvWriter.SharedKernel.Endpoints;
using ExcellentCvWriter.SharedKernel.Infrastructure.Authorization;
using ExcellentCvWriter.SharedKernel.Infrastructure.Authorization.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Users.Domain.Roles;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Users.GetUserById;

public sealed class GetUserById : EndpointBase
    .WithRequest<Guid>
    .WithResult<IResult>
{
    public override IEndpointRouteBuilder AddEndpoint(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapGet(UsersRoutes.GetById, HandleAsync)
            .Produces<UserResponse>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .RequirePermission(nameof(Permission.ReadUser))
            .RequireAuthorization();

        return routeGroupBuilder;
    }

    public override async Task<IResult> HandleAsync(Guid userId, ISender sender, CancellationToken cancellationToken)
    {
        // todo implement
        var result = Result.Success(Guid.NewGuid());
        return result.IsSuccess ? TypedResults.Ok(result.Value) : TypedResults.NotFound();
    }
}