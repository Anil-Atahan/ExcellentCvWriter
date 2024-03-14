using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using ExcellentCvWriter.SharedKernel.Persistence.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExcellentCvWriter.SharedKernel.Persistence;

internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.ConfigureOptions<ConnectionStringSetup>();
        services.AddTransientAsMatchingInterfaces(AssemblyReference.Assembly);
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}