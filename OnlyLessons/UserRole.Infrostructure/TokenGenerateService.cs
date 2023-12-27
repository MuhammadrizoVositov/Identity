
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;

public class TokenGenerateService : ITokenGeneratorService
{
    public string SecurityKey = "5129ACC7-F4A6-4459-B952-A0A56279444F";
    public string GenerateToken(User user)
    {
        var jwtToken = GetJwtSecurityToken(user);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public List<Claim> GetClaims(User user)
    {
        return new List<Claim> {
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Email,user.EmailAdress)
            };
    }

    public JwtSecurityToken GetJwtSecurityToken(User user)
    {
        var claims = GetClaims(user);

        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        var credentials = new SigningCredentials(security,SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer:"System.App",
            audience:"System.ClientApp",
            notBefore:DateTime.Now,
            expires:DateTime.Now.AddDays(1),
            claims:claims,
            signingCredentials:credentials);
    }
}
