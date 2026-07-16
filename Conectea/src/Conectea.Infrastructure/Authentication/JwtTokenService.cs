using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Conectea.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Conectea.Infrastructure.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _settings;


    public JwtTokenService(JwtSettings settings)
    {
        _settings = settings;
    }


    public string GenerateToken(Guid userId, string email)
    {
        var claims = new[]
    {
        new Claim(
            JwtRegisteredClaimNames.Sub,
            userId.ToString()
        ),

        new Claim(
            ClaimTypes.NameIdentifier,
            userId.ToString()
        ),

        new Claim(
            JwtRegisteredClaimNames.Email,
            email
        )
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _settings.ExpirationMinutes),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}