using Kesh.Application.Common.Notification.Model;
using Kesh.Domain.Common.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Notification.Service;
public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(
       EmailNotificationRequest request,
       CancellationToken cancellationToken = default
   );
}
