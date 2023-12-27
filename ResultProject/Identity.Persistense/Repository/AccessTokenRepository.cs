using Identity.Domain.Entities;
using Identity.Persistense.DataCantext;
using Identity.Persistense.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Persistense.Repository;
public class AccessTokenRepository : EntityRepositoryBase<AccessToken, IdentityDbContext>,IAccessTokenRepository
{
    public AccessTokenRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<AccessToken> CreateAsync(AccessToken token, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(token, saveChanges, cancellationToken);
    }
}
