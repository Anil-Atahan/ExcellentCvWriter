using Modules.Users.Domain.Users;

namespace Modules.Users.Application.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}

