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

        // var idClaim = _httpContextAccessor.HttpContext.User.Claims
        //     .First(c => c.Type == ClaimTypes.NameIdentifier);
        //
        // var permissionClaim = _httpContextAccessor.HttpContext.User.Claims
        //     .First(c => c.Type == "permissions");
        //
        // var roles = _httpContextAccessor.HttpContext.User.Claims
        //     .First(c => c.Type == ClaimTypes.Role);


        var id = GetClaimValue(JwtRegisteredClaimNames.Jti)
            .Select(v => Guid.Parse(v))
            .First();
        
        var permissions = GetClaimValue("permissions");
        var roles = GetClaimValue(ClaimTypes.Role);

        return new CurrentUser(
            Id: id,
            Permissions: permissions,
            Roles: roles
        );
    }

    private IReadOnlyList<string> GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext.User.Claims
            .Where(c => c.Type == claimType)
            .Select(c => c.Value)
            .ToList();
    }
}