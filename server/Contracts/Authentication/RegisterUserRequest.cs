namespace Contracts.Authentication;

public record RegisterUserRequest(string Name, string Email, string Password);