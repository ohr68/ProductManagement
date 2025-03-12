using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Jwt.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Common.Models;

namespace Auth.Jwt.Services;

public class JwtService(IOptions<JwtSettings> jwtSettings) : IJwtService
{
    public string GenerateToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, username),
                new Claim("role", "User") // Custom claims
            ]),
            Expires = DateTime.UtcNow.Add(jwtSettings.Value.Expiration),
            Issuer = jwtSettings.Value.Issuer!,
            Audience = jwtSettings.Value.Audience!,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}