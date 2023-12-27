using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Common.Enums;
using UserRole.Domain.Entites;

namespace UserRole.Application.Services;
public interface IAuthService
{
    ValueTask<bool> RegisterAsync(User user,bool asNoTracking=true,CancellationToken cancellationToken=default);
    ValueTask<string> LoginAsync(User user, bool asNoTracking=true, CancellationToken cancellationToken=default);
    ValueTask<User> GiveRole(Guid userId,Role role);
}
