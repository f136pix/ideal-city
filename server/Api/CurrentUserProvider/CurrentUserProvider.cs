using System.Security.Claims;
using Application._Common.Interfaces;
using Application._Common.Models;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Api.CurrentUserProvider;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            Console.WriteLine("HTTP CONTEXT MUST NOT BE NULL");
            throw new Exception("HttpContext is null");
        }
        
        var claim = _httpContextAccessor.HttpContext.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier);

        return new CurrentUser(Guid.Parse(claim.Value));

    }
}