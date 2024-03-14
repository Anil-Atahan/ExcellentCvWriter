using Microsoft.AspNetCore.Authorization;

namespace ExcellentCvWriter.SharedKernel.Infrastructure.Authorization.Attributes;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
        
    }
}