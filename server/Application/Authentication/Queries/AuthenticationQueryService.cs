using Application._Common.Interfaces.Authentication;
using ErrorOr;
using Microsoft.Identity.Client;

namespace Application._Common.Services.Authentication;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private IJwtTokenGenerator _tokenGenerator;

    public AuthenticationQueryService(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        throw new NotImplementedException();
    }
}