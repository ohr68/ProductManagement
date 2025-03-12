namespace Auth.Jwt.Interfaces;

public interface IJwtService
{
    string GenerateToken(string username);
}