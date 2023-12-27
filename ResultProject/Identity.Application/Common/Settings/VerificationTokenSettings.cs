using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Settings;
public class VerificationTokenSettings
{
    public string IdentityVerificationTokenPurpose { get; set; } = default!;

    public int IdentityVerificationExpirationDurationInMinutes { get; set; }
}
