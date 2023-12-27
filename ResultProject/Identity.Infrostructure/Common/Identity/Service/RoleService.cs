using Identity.Application.Common.Identity.Service;
using Identity.Domain.Entities;
using Identity.Domain.Enum;
using Identity.Persistense.Repository.Interface;
using Kesh.Application.Common.Queriyng;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrostructure.Common.Identity.Service;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public ValueTask<IList<Role>> GetByFilterAsync(FilterPagination filterOptions, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default)=>
    
        await _roleRepository.Get(asNoTracking: asNoTracking)
            .SingleOrDefaultAsync(role => role.Type == roleType, cancellationToken);

    public ValueTask<Guid> GetDefaultRoleId(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
