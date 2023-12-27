using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Identity.Service;
public interface IAccessTokenService
{
    ValueTask<AccessToken> CreateAsync(
      Guid userId,
      string value,
      bool saveChanges = true,
      CancellationToken cancellationToken = default
  );
}
