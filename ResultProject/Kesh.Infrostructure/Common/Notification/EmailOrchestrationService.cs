using Kesh.Application.Common.Identity.Service;
using Kesh.Application.Common.Notification.Model;
using Kesh.Application.Common.Notification.Service;
using Kesh.Domain.Common.Exeptions;
using Kesh.Infrostructure.Common.Setting;
using Microsoft.Extensions.Options;

namespace Kesh.Infrostructure.Common.Notification;
public class EmailOrchestrationService(IOptions<SmtpEmailSenderSettings> smtpSenderSettings, IUserService userService) : IEmailOrchestrationService
{
    public ValueTask<FuncResult<bool>> SendAsync(EmailNotificationRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
