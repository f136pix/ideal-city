using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Users;

public record UpdateUserCommand(
    Guid UserId,
    string? Name,
    string? Email,
    Guid? CityId,
    string? Password,
    string? ProfilePicture,
    string? Bio
) : IRequest<ErrorOr<User>>;