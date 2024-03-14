using ExcellentCvWriter.SharedKernel.Domain.Primitives;

namespace Modules.Users.Domain.Users.Events;

/// <summary>
/// Represents the domain event that is raised when a new user is registered.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="UserId">The user identifier.</param>
/// <param name="Email">The email.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
/// <param name="Roles">The roles.</param>
public sealed record UserRegisteredDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserId UserId,
    string Email,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : DomainEvent(Id, OccurredOnUtc);
