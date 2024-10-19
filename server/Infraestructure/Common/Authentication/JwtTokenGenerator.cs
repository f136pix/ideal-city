using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application._Common.Interfaces.Authentication;
using Contracts.Subscriptions;
using Domain.User.ValueObject;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application._Common.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(Guid userId, string name, Subscription userSubscription)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("subscription", userSubscription.SubscriptionType.Value.ToString())
            // new Claim("permissions", "city:create"),
            // new Claim("permissions", "city:update")
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            SecurityAlgorithms.HmacSha256
        );

        var securityKey = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityKey);
    }
}