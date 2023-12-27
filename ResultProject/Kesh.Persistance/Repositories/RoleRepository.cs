using Kesh.Domain.Entities;
using Kesh.Persistance.DataCantext;
using Kesh.Persistance.Repositories.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Persistance.Repositories;
public class RoleRepository(IdentityDbContext dbContext) : EntityRepositoryBase<Role, IdentityDbContext>(dbContext), IRoleRepository
{
    public IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate, bool asNoTracking)
    {
       return base.Get(predicate, asNoTracking);
    }
}
