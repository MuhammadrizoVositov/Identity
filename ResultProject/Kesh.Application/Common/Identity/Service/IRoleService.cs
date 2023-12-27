using Kesh.Application.Common.Queriyng;
using Kesh.Domain.Entities;
using Kesh.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Identity.Service;
public interface IRoleService
{
    ValueTask<IList<Role>> GetByFilterAsync(
    FilterPagination filterOptions,
    bool asNoTracking = false,
    CancellationToken cancellationToken = default
);


    ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Guid> GetDefaultRoleId(CancellationToken cancellationToken = default);
}
