using Modules.Users.Domain.Roles;
using Modules.Users.Domain.Users;

namespace Modules.Users.Infrastructure.Authorization.Models;

public sealed class UserRolesResponse
{
    public UserId Id { get; init; }
    public List<Role> Roles { get; init; } = [];
}