using Application._Common.Interfaces;
using Application._Common.Interfaces.Authentication;
using Application.Authentication.Common;
using Domain.User;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
{
    private IUserRepository _userRepository;

    private IJwtTokenGenerator _tokenGenerator;

    public RegisterUserCommandHandler(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _userRepository.GetByProperty("Email", request.Email) is User user)
        {
            return Error.Validation(description: "Email already exists");
        }

        User newUser = User.Create(request.Name, request.Email, null, null, request.Password, null, null);
        string token = _tokenGenerator.GenerateToken(newUser.Id.Value, newUser.Email);

        return new AuthenticationResult(newUser, token);
    }
}