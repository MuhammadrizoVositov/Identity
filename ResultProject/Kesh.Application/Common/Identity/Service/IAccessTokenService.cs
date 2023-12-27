using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Identity.Service;
public interface IAccessTokenService
{
    ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);

    ValueTask RevokeAsync(Guid accessTokenId, CancellationToken cancellationToken = default);
    ValueTask<AccessToken?> UpdateAsync(AccessToken createdToken, CancellationToken cancellationToken = default);
}
