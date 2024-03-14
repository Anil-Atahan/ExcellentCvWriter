using ExcellentCvWriter.SharedKernel.Application.Cache;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;
using Modules.Users.Infrastructure.Authorization.Models;
using Modules.Users.Persistence;

namespace Modules.Users.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly UsersDbContext _dbContext;
    private readonly ICacheService _cacheService;

    public AuthorizationService(UsersDbContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        var cacheKey = $"auth:roles-{identityId}";
        
        var cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);
        
        if (cachedRoles is not null)
        {
            return cachedRoles;
        }
        
        var roles = await _dbContext.Set<User>()
            .Where(user => user.IdentityProviderId == identityId)
            .Select(user => new UserRolesResponse
            {
                Id = user.Id,
                Roles = user.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    public async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        
        var cacheKey = $"auth:permissions-{identityId}";
        
        var cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);
        
        if (cachedPermissions is not null)
        {
            return cachedPermissions;
        }
        
        var permissions = await _dbContext.Set<User>()
            .Where(user => user.IdentityProviderId == identityId)
            .SelectMany(user => user.Roles.Select(role => role.Permissions))
            .FirstAsync();

        var permissionsSet = permissions.Select(p => p.Name).ToHashSet();
        
        await _cacheService.SetAsync(cacheKey, permissionsSet);
        
        return permissionsSet;
    }
}