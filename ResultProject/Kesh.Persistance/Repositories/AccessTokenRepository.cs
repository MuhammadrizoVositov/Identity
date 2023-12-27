using Kesh.Domain.Common.Cashing;
using Kesh.Domain.Entities;
using Kesh.Persistance.Cashing.Brokers;
using Kesh.Persistance.Repositories.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Persistance.Repositories;
public class AccessTokenRepository(ICacheBroker cacheBroker) : IAccessTokenRepository
{
    public async ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = accessToken.ExpiryTime - DateTimeOffset.UtcNow
        };

        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<AccessToken>(accessTokenId.ToString(), cancellationToken: cancellationToken);
    }

    public ValueTask RevokeAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.DeleteAsync(accessTokenId.ToString(), cancellationToken: cancellationToken);
    }
}
