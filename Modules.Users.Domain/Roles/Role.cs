using ExcellentCvWriter.SharedKernel.Domain.Primitives;
using Modules.Users.Domain.Users;

namespace Modules.Users.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, "Registered");
    public static readonly Role Administrator = new(100, "Administrator");
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    private Role(int id, string name)
        : base(id, name)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    private Role()
    {
    }

    public ICollection<User> Users { get; init; } = new List<User>();
    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
}