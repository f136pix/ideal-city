namespace Contracts.Users;

public record UserResponse(
    string Id,
    string Name,
    string Email,
    string SubscriptionId,
    string CityId,
    string Password,
    string? ProfilePicture,
    string? Bio,
    List<string> PostIds
);