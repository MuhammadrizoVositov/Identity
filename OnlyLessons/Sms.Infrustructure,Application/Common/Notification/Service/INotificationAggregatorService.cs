using Sms.Infrastructure.Domain.Common.Exeptions;
using Sms.Infrustructure.Application.Common.Notification.Model;

namespace Sms.Infrustructure.Application.Common.Notification.Service;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
        );
}
