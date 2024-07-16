namespace Application._Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string name);
}