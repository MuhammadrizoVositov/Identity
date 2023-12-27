using Identity.Domain.Entities;

namespace Identity.Persistense.Repository.Interface;
public interface IAccessTokenRepository
{
    ValueTask<AccessToken> CreateAsync(
     AccessToken token,
     bool saveChanges = true,
     CancellationToken cancellationToken = default
 );
}
