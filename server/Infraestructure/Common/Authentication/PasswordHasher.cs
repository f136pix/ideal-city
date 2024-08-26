using Domain._Common.Interfaces;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;

namespace Application._Common.Services.Authentication;

public class PasswordHasher : IPasswordHasher
{
    IPasswordHasher<User> _passwordHasher;

    public PasswordHasher(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool IsCorrectPassword(string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        if (result == PasswordVerificationResult.Success)
        {
            return true;
        }

        return false;
    }
}