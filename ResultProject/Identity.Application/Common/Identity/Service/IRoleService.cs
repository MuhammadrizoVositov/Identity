using Identity.Domain.Entities;
using Identity.Domain.Enum;
using Kesh.Application.Common.Queriyng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Identity.Service;
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
