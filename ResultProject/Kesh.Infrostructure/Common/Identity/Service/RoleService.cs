using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kesh.Application.Common.Extesion;
using Kesh.Application.Common.Queriyng;
using Kesh.Application.Common.Identity.Service;
using Kesh.Persistance.Repositories.Interfase;
using Kesh.Domain.Entities;
using Kesh.Domain.Enums;
namespace Kesh.Infrostructure.Common.Identity.Service;
public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async ValueTask<IList<Role>> GetByFilterAsync(FilterPagination filterOptions, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await roleRepository.Get(asNoTracking: asNoTracking).ApplyPagination(filterOptions).ToListAsync(cancellationToken: cancellationToken);
    }

    public async ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await roleRepository.Get(role => role.Type == roleType, asNoTracking: asNoTracking).FirstOrDefaultAsync(cancellationToken);

    }


    public async ValueTask<Guid> GetDefaultRoleId(CancellationToken cancellationToken = default)
    {
        var roleId = await roleRepository.Get(role => role.Type == RoleType.User, true)
            .Select(role => role.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return roleId != Guid.Empty ? roleId : throw new InvalidOperationException();
    }
}
