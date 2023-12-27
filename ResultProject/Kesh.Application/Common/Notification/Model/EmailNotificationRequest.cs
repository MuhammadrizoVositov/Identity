using Kesh.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Notification.Model;
public class EmailNotificationRequest:NotificationRequest
{
    public EmailNotificationRequest() => Type = NotificationType.Email;
}
