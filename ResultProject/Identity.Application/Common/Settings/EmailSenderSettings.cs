using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Settings;
public class EmailSenderSettings
{
    public string Host { get; set; } = default!;

    public int Port { get; set; }

    public string CredentialAddress { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string TestValue { get; set; } = string.Empty;
}
