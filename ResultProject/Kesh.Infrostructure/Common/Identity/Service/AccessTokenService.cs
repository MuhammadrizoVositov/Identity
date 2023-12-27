using Kesh.Application.Common.Identity.Service;
using Kesh.Domain.Entities;
using Kesh.Persistance.Repositories.Interfase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Identity.Service;
public class AccessTokenService(IAccessTokenRepository accessTokenRepository) : IAccessTokenService
{
    public ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.CreateAsync(accessToken, saveChanges, cancellationToken);
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.GetByIdAsync(accessTokenId, cancellationToken);
    }

    public ValueTask RevokeAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.RevokeAsync(accessTokenId, cancellationToken);
    }

    public ValueTask<AccessToken?> UpdateAsync(AccessToken createdToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
