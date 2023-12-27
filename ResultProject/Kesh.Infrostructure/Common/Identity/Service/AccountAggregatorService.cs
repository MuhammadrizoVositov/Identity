using Kesh.Application.Common.Identity.Service;
using Kesh.Application.Common.Notification.Model;
using Kesh.Application.Common.Notification.Service;
using Kesh.Application.Common.Verification.Service;
using Kesh.Domain.Constants;
using Kesh.Domain.Entities;
using Kesh.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Identity.Service;
public class AccountAggregatorService(
    IUserService userService,
    IUserInfoVerificationCodeService userInfoVerificationCodeService,
    IEmailOrchestrationService emailOrchestrationService
) : IAccountAggregatorService
{
    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        // create user
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);

        // send welcome email
        var systemUser = await userService.GetSystemUserAsync(cancellationToken: cancellationToken);
        await emailOrchestrationService.SendAsync(
            new EmailNotificationRequest
            {
                SenderUserId = systemUser.Id,
                ReceiverUserId = createdUser.Id,
                TemplateType = NotificationTemplateType.WelcomeNotification,
                Variables = new Dictionary<string, string>
                {
                    { NotificationTemplateConstants.UserNamePlaceholder, createdUser.FirstName }
                }
            },
            cancellationToken
        );

        // send verification email
        var verificationCode = await userInfoVerificationCodeService.CreateAsync(
            VerificationCodeType.EmailAddressVerification,
            createdUser.Id,
            cancellationToken
        );

        await emailOrchestrationService.SendAsync(
            new EmailNotificationRequest
            {
                SenderUserId = systemUser.Id,
                ReceiverUserId = createdUser.Id,
                TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
                Variables = new Dictionary<string, string>
                {
                    { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink }
                }
            },
            cancellationToken
        );

        return true;
    }
}
