using ExcellentCvWriter.SharedKernel.Common.Results;

namespace Modules.Users.Application.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}