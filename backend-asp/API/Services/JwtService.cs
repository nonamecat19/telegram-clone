using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class JwtService(IConfiguration configuration)
{
    public string GenerateToken(string userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new("userId", userId),
        };
        
        
        var token = new JwtSecurityToken(
            configuration["JwtSettings:Issuer"],
            configuration["JwtSettings:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GetUserId(ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault();
        if (claim == null)
        {
            throw new UnauthorizedAccessException("User ID not found in claims");
        }
        return claim.Value;
    }
}