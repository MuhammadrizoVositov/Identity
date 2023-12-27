using Kesh.Application.Common.Identity.Service;
using Kesh.Application.Common.Verification.Service;
using Kesh.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Verification.Service;
public class VerificationProcessingService(IUserInfoVerificationCodeService userInfoVerificationCodeService, IUserService userService) : IVerificationProcessingService
{
    public async ValueTask<bool> Verify(string code, CancellationToken cancellationToken)
    {
        var userActionVerificationCode = await userInfoVerificationCodeService.GetByCodeAsync(code, cancellationToken);

        if (!userActionVerificationCode.IsValid) return false;

        var user = await userService.GetByIdAsync(userActionVerificationCode.Code.UserId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException();

        user.IsEmailAddressVerified = true;
        await userService.UpdateAsync(user, false, cancellationToken);
        await userInfoVerificationCodeService.DeactivateAsync(userActionVerificationCode.Code.Id, cancellationToken: cancellationToken);

        return true;
    }

    private async ValueTask<bool> VerifyUserInfoAsync(string code, CancellationToken cancellationToken = default)
    {
        var userInfoVerificationCode = await userInfoVerificationCodeService.GetByCodeAsync(code, cancellationToken);

        if (!userInfoVerificationCode.IsValid) return false;

        var user = await userService.GetByIdAsync(userInfoVerificationCode.Code.UserId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException();

        switch (userInfoVerificationCode.Code.CodeType)
        {
            case VerificationCodeType.EmailAddressVerification:
                user.IsEmailAddressVerified = true;
                await userService.UpdateAsync(user, false, cancellationToken);
                break;
            default: throw new NotSupportedException();
        }

        await userInfoVerificationCodeService.DeactivateAsync(userInfoVerificationCode.Code.Id, cancellationToken: cancellationToken);

        return true;
    }
}
