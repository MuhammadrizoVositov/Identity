using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Application.Services;
using UserRole.Domain.Common.Enums;
using UserRole.Domain.Entites;

namespace UserRole.Infrostructure;
public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenGeneratorService _tokenGenerateService;

    public AuthService(ITokenGeneratorService tokenGenerateService, IUserService userService )
    {
        _userService = userService;
        _tokenGenerateService = tokenGenerateService;
    }

    public async ValueTask<User> GiveRole(Guid userId, Role role)
    {
        var user = await _userService.GetByIdAsync(userId, true);
        
        user.Role = role;
        return await _userService.UpdateAsync(user);
        
    }

    public async ValueTask<string> LoginAsync(User user, bool asNoTracking, CancellationToken cancellationToken)
    {
      var newuser=await  _userService.GetByEmail(user.EmailAdress);
        return _tokenGenerateService.GenerateToken(newuser);
    }

    public async ValueTask<bool> RegisterAsync(User user, bool asNoTracking, CancellationToken cancellationToken)
    {
        var newuser = await _userService.CreateAsync(user);
        return newuser is not null;
    }
}
