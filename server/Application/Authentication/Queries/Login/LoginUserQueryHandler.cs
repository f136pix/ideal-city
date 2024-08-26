using Application._Common.Interfaces;
using Application._Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Domain._Common.Interfaces;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.Login;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUserQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByProperty("Email", request.Email);

        if (user is null) return Error.Unauthorized(description: "Invalid User credentials");

        var result = user.IsCorrectPasswordHash(request.Password, _passwordHasher);

        if (result is false)
        {
            return Error.Unauthorized(description: "Invalid User credentials");
        }

        string token = _tokenGenerator.GenerateToken(user.Id.Value, user.Email);
        return new AuthenticationResult(user, token);
    }
}