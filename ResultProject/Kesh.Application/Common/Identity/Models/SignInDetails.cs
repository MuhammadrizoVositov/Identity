﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kesh.Application.Common.Identity.Models;
public class SignInDetails
{
    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}
