﻿using FluentValidation;
using Kesh.Application.Common.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Validators;
public class CredentialDetailsValidator:AbstractValidator<CredentialDetails>
{
    public CredentialDetailsValidator()
    {
        RuleFor(credential => credential.Password)
            .Custom((password, context) =>
            {
                if (context.RootContextData.TryGetValue("PersonalInformation", out var userInfoObj) &&
                    userInfoObj is IEnumerable<string> userInfo &&
                    userInfo.Any(info => !string.IsNullOrEmpty(info) && password.Contains(info)))
                    context.AddFailure("Password must not contain user public information.");
            });
    }
}
