using ExcellentCvWriter.Api.Extensions;
using ExcellentCvWriter.SharedKernel.Endpoints;
using ExcellentCvWriter.SharedKernel.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services
    .InstallServicesFromAssemblies(
        builder.Configuration,
        ExcellentCvWriter.Api.AssemblyReference.Assembly,
        ExcellentCvWriter.SharedKernel.AssemblyReference.Assembly)
    .InstallModulesFromAssemblies(
        builder.Configuration,
        Modules.Users.Infrastructure.AssemblyReference.Assembly,
        Modules.Notifications.Infrastructure.AssemblyReference.Assembly);

builder.Host.UseSerilogWithConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseCors(corsPolicyBuilder =>
        corsPolicyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

app
    .UseSerilogRequestLogging()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

var routeGroupBuilder = app.MapGroup("api/v1");
routeGroupBuilder
    .InstallEndpoints(
        Modules.Users.Endpoints.AssemblyReference.Assembly);
app.MapControllers();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();