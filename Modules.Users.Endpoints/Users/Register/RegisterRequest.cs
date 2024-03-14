namespace Modules.Users.Endpoints.Users.Register;

public sealed record RegisterRequest(string Email, string Password, string FirstName, string LastName);