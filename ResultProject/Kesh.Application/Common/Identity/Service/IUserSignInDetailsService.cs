using Kesh.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Identity.Service;
public interface IUserSignInDetailsService
{
    ValueTask<bool> ValidateSignInLocation(CancellationToken cancellationToken = default);

    ValueTask<UserSignInDetails?> GetLastSignInDetailsAsync(Guid userId, bool asNoTracking, CancellationToken cancellationToken = default);

    ValueTask RecordSignInAsync(bool saveChanges = true, CancellationToken cancellationToken = default);
}
