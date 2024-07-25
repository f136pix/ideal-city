namespace Contracts.Users;

public record UpdateUserRequest(
    Guid UserId,
    string? Name,
    string? Email,
    Guid? CityId,
    string? Password,
    string? ProfilePicture,
    string? Bio
);