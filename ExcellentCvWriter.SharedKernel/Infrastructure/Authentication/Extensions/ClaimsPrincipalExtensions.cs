using System.Security.Claims;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Authentication.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
               throw new ApplicationException("User identity is unavailable");
    }
}