using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Identity.Service;
public interface IAccountService
{
    ValueTask<bool> VerificateAsync(string token, CancellationToken cancellationToken = default);

    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}
