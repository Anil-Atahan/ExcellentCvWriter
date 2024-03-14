using ExcellentCvWriter.SharedKernel.Common.Results;
using ExcellentCvWriter.SharedKernel.Domain.Primitives;
using ExcellentCvWriter.SharedKernel.Domain.Time;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.Users.Events;

namespace Modules.Users.Domain.Users;

public sealed class User : Entity<UserId>, IAuditable
{
    private readonly HashSet<Role> _roles = new(); 
    private User(UserId id,
        string email,
        string firstName,
        string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    private User()
    {

    }
        
    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityProviderId { get; private set; }
    
    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles.ToList();
    
    /// <summary>
    /// Creates a new user with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="identityProviderId">The identity provider identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new user instance.</returns>
    public static User Create(UserId id, string email, string firstName, string lastName)
    {
        var user = new User(id, email, firstName, lastName);

        user._roles.Add(Role.Registered);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user._roles.Select(role => role.Name).ToHashSet()));

        return user;
    }
    
    public void SetIdentityProviderId(string identityProviderId)
    {
        IdentityProviderId = identityProviderId;
    }
}