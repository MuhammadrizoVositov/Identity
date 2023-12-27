using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Persistance.Repositories.Interfase;
public interface IUserSignInDetailsRepository
{
    IQueryable<UserSignInDetails> Get(Expression<Func<UserSignInDetails, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<UserSignInDetails> CreateAsync(UserSignInDetails userSignInDetails, bool saveChanges = true, CancellationToken cancellationToken = default);
}
