﻿using FluentValidation;
using Kesh.Application.Common.Verification.Service;
using Kesh.Domain.Entities;
using Kesh.Domain.Enums;
using Kesh.Infrostructure.Common.Setting;
using Kesh.Persistance.Repositories.Interfase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Verification.Service;
public class UserInfoVerificationCodeService(
    IOptions<VerificationSettings> verificationSettings,
    IValidator<UserInfoVerificationCode> userInfoVerificationCodeValidator,
    IUserInfoVerificationCodeRepository userInfoVerificationCodeRepository
) : IUserInfoVerificationCodeService

{
    private readonly VerificationSettings _verificationSettings = verificationSettings.Value;
    public async ValueTask<UserInfoVerificationCode> CreateAsync(VerificationCodeType codeType, Guid userId, CancellationToken cancellationToken = default)
    {
        var verificationCodeValue = default(string);

        do
        {
            var verificationCodes = Generate();
            var existingCodes = await userInfoVerificationCodeRepository.Get(code => verificationCodes.Contains(code.Code))
                .ToListAsync(cancellationToken: cancellationToken);

            verificationCodeValue = verificationCodes.Except(existingCodes.Select(code => code.Code)).FirstOrDefault() ??
                                    throw new InvalidOperationException("Verification code generation failed.");
        } while (string.IsNullOrEmpty(verificationCodeValue));

        var verificationCode = new UserInfoVerificationCode
        {
            Code = verificationCodeValue,
            CodeType = codeType,
            UserId = userId,
            IsActive = true,
            VerificationLink = $"{_verificationSettings.VerificationLink}/{verificationCodeValue}",
            ExpiryTime = DateTimeOffset.UtcNow.AddSeconds(_verificationSettings.VerificationCodeExpiryTimeInSeconds)
        };

        var validationResult = userInfoVerificationCodeValidator.Validate(verificationCode);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        await userInfoVerificationCodeRepository.CreateAsync(verificationCode, cancellationToken: cancellationToken);

        return verificationCode;
    }
    public ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userInfoVerificationCodeRepository.DeactivateAsync(codeId, saveChanges, cancellationToken);
    }

    public IList<string> Generate()
    {
        var codes = new List<string>();

        for (var index = 0; index < 10; index++)
            codes.Add(string.Join("", RandomNumberGenerator.GetBytes(_verificationSettings.VerificationCodeLength).Select(@char => @char % 10)));

        return codes;
    }

    public async ValueTask<(UserInfoVerificationCode Code, bool IsValid)> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var verificationCode = await userInfoVerificationCodeRepository.Get(verificationCode => verificationCode.Code == code, true)
             .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException();

        return (verificationCode, verificationCode.IsActive && verificationCode.ExpiryTime > DateTimeOffset.UtcNow);
    }
}



