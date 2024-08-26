namespace Domain._Common.Interfaces;

public interface IPasswordHasher
{
    bool IsCorrectPassword(string password, string hashedPassword);   
}