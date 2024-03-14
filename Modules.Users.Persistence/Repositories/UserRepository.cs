using ExcellentCvWriter.SharedKernel.Application.ServiceLifetimes;
using ExcellentCvWriter.SharedKernel.Common.Results;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;

namespace Modules.Users.Persistence.Repositories;

internal class UserRepository : IUserRepository, IScoped
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<Result<User>> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Set<User>().Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        return Result.Create(user);
    }

    /// <inheritdoc />
    public async Task<Result<bool>> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var exists = await _dbContext.Set<User>().AnyAsync(user => user.Email == email, cancellationToken);
        return Result.Success(exists);
    }

    /// <inheritdoc />
    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);

        _dbContext.AttachRange(user.Roles);
    }
}