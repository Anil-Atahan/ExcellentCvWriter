using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using ExcellentCvWriter.SharedKernel.Persistence.Extensions;
using ExcellentCvWriter.SharedKernel.Persistence.Interceptors;
using ExcellentCvWriter.SharedKernel.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Modules.Users.Persistence;
using Modules.Users.Persistence.Constants;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module persistence service installer.
/// </summary>
internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();
        services.AddDbContext<UsersDbContext>((serviceProvider, options) =>
        {
            ConnectionStringOptions connectionString = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

            options
                .UseNpgsql(
                    connectionString,
                    dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Users))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(
                    serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!,
                    serviceProvider.GetService<UpdateAuditableEntitiesInterceptor>()!);
        });
    }
}
