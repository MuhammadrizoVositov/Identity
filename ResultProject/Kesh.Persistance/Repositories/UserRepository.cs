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
public class UserRepository(IdentityDbContext dbContext) : EntityRepositoryBase<User, IdentityDbContext>(dbContext), IUserRepository
{
    ValueTask<User> IUserRepository.CreateAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
      return  base.CreateAsync(user, saveChanges, cancellationToken);
    }

    IQueryable<User> IUserRepository.Get(Expression<Func<User, bool>>? predicate, bool asNoTracking)
    {
       return base.Get(predicate, asNoTracking);
    }

    ValueTask<User?> IUserRepository.GetByIdAsync(Guid userId, bool asNoTracking, CancellationToken cancellationToken)
    {
       return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    ValueTask<User> IUserRepository.UpdateAsync(User user, bool saveChanges, CancellationToken cancellationToken)
    {
      return  base.UpdateAsync(user, saveChanges, cancellationToken);
    }
}
