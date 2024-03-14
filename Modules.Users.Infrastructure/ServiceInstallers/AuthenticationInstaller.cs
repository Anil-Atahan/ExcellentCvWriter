
using ExcellentCvWriter.SharedKernel.Infrastructure.Authentication;
using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Modules.Users.Application.Authentication;
using Modules.Users.Infrastructure.Authentication;
using KeycloakOptions = Modules.Users.Infrastructure.Authentication.KeycloakOptions;

namespace Modules.Users.Infrastructure.ServiceInstallers;

internal sealed class AuthenticationInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }
}