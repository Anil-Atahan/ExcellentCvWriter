using ExcellentCvWriter.SharedKernel.Common.Errors;
using ExcellentCvWriter.SharedKernel.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Users.Application.Users.Register;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Users.Register;

public sealed class Register : EndpointBase
    .WithRequest<RegisterRequest>
    .WithResult<IResult>
{
    public override IEndpointRouteBuilder AddEndpoint(IEndpointRouteBuilder routeGroupBuilder)
    {
        routeGroupBuilder.MapPost(UsersRoutes.Register, HandleAsync)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest);
        
        return routeGroupBuilder;
    }
    
    public override async Task<IResult> HandleAsync(RegisterRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.Email, request.Password, request.FirstName, request.LastName);

        var result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.CreatedAtRoute(result.Value, nameof(GetUserById), new { userId = result.Value });
    }
}

