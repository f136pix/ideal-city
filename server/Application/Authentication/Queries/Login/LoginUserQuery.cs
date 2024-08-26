using ErrorOr;
using MediatR;
using Microsoft.Identity.Client;

namespace Application.Authentication.Queries.Login;

public record LoginUserQuery(string Email, string Password) : IRequest<ErrorOr<Common.AuthenticationResult>>;