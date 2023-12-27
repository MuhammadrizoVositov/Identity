using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrostructure.Common.Identity.Service;
public class VerificationCodeGeneratorService
{
    private static string GenerateOnlyDigitCode(int length)
    {
        var code = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            code.Append(RandomNumberGenerator.GetInt32(0, 10));
        }

        return code.ToString();
    }
}
