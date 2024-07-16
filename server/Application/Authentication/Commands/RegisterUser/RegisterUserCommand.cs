using Application.Authentication.Common;
using Domain.CityAggregate;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands;

public record RegisterUserCommand(string Name, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;