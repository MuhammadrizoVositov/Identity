﻿using FluentValidation;
using Kesh.Application.Common.Identity.Models;
using Kesh.Infrostructure.Common.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Infrostructure.Common.Validators;
public class SignInDetailsValidator:AbstractValidator<SignInDetails>
{
    public SignInDetailsValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .Matches(validationSettingsValue.EmailAddressRegexPattern);

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
