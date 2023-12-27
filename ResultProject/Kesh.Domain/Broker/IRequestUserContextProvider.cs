using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Domain.Broker;
public interface IRequestUserContextProvider
{
    Guid GetUserIdAsync(CancellationToken cancellationToken = default);
}
