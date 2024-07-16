using ErrorOr;
using Microsoft.Identity.Client;

namespace Application._Common.Interfaces.Authentication;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}