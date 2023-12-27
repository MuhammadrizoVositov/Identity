
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;

public interface ITokenGeneratorService
{
    string GenerateToken(User user);
    JwtSecurityToken GetJwtSecurityToken(User user);
    List<Claim>GetClaims(User user);
}
