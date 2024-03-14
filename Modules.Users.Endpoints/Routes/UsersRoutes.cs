namespace Modules.Users.Endpoints.Routes;

internal static class UsersRoutes
{
    private const string BaseUri = "users";

    private const string ResourceId = $"userId";

    internal const string Register = $"{BaseUri}/register";
    
    internal const string GetById = $"{BaseUri}/{{{ResourceId}:guid}}";
}
