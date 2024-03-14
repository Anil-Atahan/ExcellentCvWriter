using ExcellentCvWriter.SharedKernel.Common.Results;
using MediatR;

namespace Modules.Users.Application.Users.Register;

/// <summary>
/// Represents the command for registering a new user.
/// </summary>
/// <param name="Email">The email.</param>
/// <param name="Password">The password.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : IRequest<Result<Guid>>;
