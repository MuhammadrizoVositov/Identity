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
public class UserSignInDetailsRepository(IdentityDbContext dbContext) : EntityRepositoryBase<UserSignInDetails, IdentityDbContext>(dbContext), IUserSignInDetailsRepository
{
    public IQueryable<UserSignInDetails> Get(Expression<Func<UserSignInDetails, bool>>? predicate = default, bool asNoTracking = false) =>
    base.Get(predicate, asNoTracking);

    public ValueTask<UserSignInDetails> CreateAsync(UserSignInDetails userSignInDetails, bool saveChanges = true, CancellationToken cancellationToken = default) =>
        base.CreateAsync(userSignInDetails, saveChanges, cancellationToken);

}



